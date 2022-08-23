using System;
using System.Globalization;
using System.Text;
using System.Data;

using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace CNO.BPA.ScanPop.DataHandler
{
   public class DataAccess : IDisposable
   {
      #region Variables
      private CustomParameters _parmsCustom = null;
      private Framework.Cryptography crypto = new
         Framework.Cryptography();
      private OracleConnection _connection = null;
      private string _connectionString = null;
      private OracleTransaction _transaction = null;      
      private string _DSN = string.Empty;
      private string _DBUser = string.Empty;
      private string _DBPass = string.Empty;

      #endregion

      #region Constructors
      public DataAccess(ref CustomParameters ParmsCustom)
      {
          _parmsCustom = ParmsCustom;

         //check to see that we have values for the db info
         if (_parmsCustom.DSN.Length != 0 && _parmsCustom.UserName.Length != 0 &&
             _parmsCustom.Password.Length != 0)
         {
             _DSN = _parmsCustom.DSN;
             _DBUser = crypto.Decrypt(_parmsCustom.UserName);
             _DBPass = crypto.Decrypt(_parmsCustom.Password);
            //build the connection string
            _connectionString = "Data Source=" + _DSN + ";Persist Security Info=True;User ID="
               + _DBUser + ";Password=" + _DBPass + "";
         }
         else
         {
            throw new ArgumentNullException("-266007825; Database connection information was "
               + "not found.");
         }
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Connects and logs in to the database, and begins a transaction.
      /// </summary>
      public void Connect()
      {
         _connection = new OracleConnection();
         _connection.ConnectionString = _connectionString;
         try
         {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
         }
         catch (Exception ex)
         {
            throw new Exception("An error occurred while connecting to the database.", ex);
         }
      }
      /// <summary>
      /// Commits the current transaction and disconnects from the database.
      /// </summary>
      public void Disconnect()
      {
         try
         {
            if (null != _connection)
            {
               _transaction.Commit();
               _connection.Close();
               _connection = null;
               _transaction = null;
            }
         }
         catch { } // ignore an error here
      }
      /// <summary>
      /// Commits all of the data changes to the database.
      /// </summary>
      internal void Commit()
      {
         _transaction.Commit();
      }
      /// <summary>
      /// Cancels the transaction and voids any changes to the database.
      /// </summary>
      public void Cancel()
      {
         _transaction.Rollback();
         _connection.Close();
         _connection = null;
         _transaction = null;
      }
      /// <summary>
      /// Generates the command object and associates it with the current transaction object
      /// </summary>
      /// <param name="commandText"></param>
      /// <param name="commandType"></param>
      /// <returns></returns>
      internal OracleCommand GenerateCommand(string commandText, System.Data.CommandType commandType)
      {
         OracleCommand cmd = new OracleCommand(commandText, _connection);
         cmd.Transaction = _transaction;
         cmd.CommandType = commandType;
         return cmd;
      }
      #endregion

      #region Public Methods
      public string getBatchNo()
      {
          try
          {
              string batchNo = string.Empty;
              Connect();
              OracleCommand cmd = GenerateCommand("bpa_apps.pkg_batch.get_batch_no", CommandType.StoredProcedure);
                cmd.BindByName = true;
                DBUtilities.CreateAndAddParameter("p_in_batch_source_code",
                 BatchDetail.ScannerID, OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_in_received_date",
                 Convert.ToDateTime(BatchDetail.ReceivedDate).ToString("yyMMdd"), OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_out_batch_no",
                OracleDbType.Varchar2, ParameterDirection.Output, 15, cmd);
              DBUtilities.CreateAndAddParameter("p_out_result",
                 OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
              DBUtilities.CreateAndAddParameter("p_out_error_message",
                 OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);
              
              cmd.ExecuteNonQuery();

              if (cmd.Parameters["p_out_result"].Value.ToString().ToUpper() == "SUCCESSFUL")
              {
                  //grab the values
                  batchNo = cmd.Parameters["p_out_batch_no"]
                     .Value.ToString();
              }
              else
              {
                  throw new Exception("-266088529; Procedure Error: " +
                     cmd.Parameters["p_out_result"].Value.ToString() + "; Oracle Error: " +
                     cmd.Parameters["p_out_error_message"].Value.ToString());
              } 
              Disconnect();
              return batchNo;
          }
          catch (Exception ex)
          {
              throw new Exception("DataHandler.DataAccess.getBatchNo: " + ex.Message);
          }
      }
      public List<string> getDepartmentAssignment(string ProcessName)
      {
          try
          {
              List<string> deptList = new List<string>();
              Connect();
              OracleCommand cmd = GenerateCommand("bpa_apps.pkg_ia.select_assigned_departments", CommandType.StoredProcedure);
                cmd.BindByName = true;
              DBUtilities.CreateAndAddParameter("p_in_job_name",
                 ProcessName, OracleDbType.Varchar2, ParameterDirection.Input, cmd);              
              DBUtilities.CreateAndAddParameter("p_out_ref_cursor",
                 DBNull.Value, OracleDbType.RefCursor, ParameterDirection.Output,
                 cmd);
              DBUtilities.CreateAndAddParameter("p_out_result",
                 OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
              DBUtilities.CreateAndAddParameter("p_out_error_message",
                 OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);
              using (OracleDataReader dataReader = cmd.ExecuteReader())
              {
                  if (cmd.Parameters["p_out_result"].Value.ToString()
                     .ToUpper() != "SUCCESSFUL")
                  {
                      throw new Exception("-266088529; Procedure Error: " +
                         cmd.Parameters["p_out_result"].Value.ToString() +
                         "; Oracle Error: " + cmd.Parameters[
                         "p_out_error_message"].Value.ToString());
                  }
                  else
                  {
                      // Always call Read before accessing data.
                      while (dataReader.Read())
                      {
                          deptList.Add(dataReader.GetString(0));
                      }
                  }
              }
              Disconnect();
              return deptList;
          }
          catch (Exception ex)
          {
              throw new Exception("DataHandler.DataAccess.getDepartmentAssignment: " + ex.Message);
          }

      }
      public DataSet getDepartmentDetails()
      {
          try
          {
              DataSet DataSetResults = new DataSet();
              Connect();
              OracleCommand cmd = GenerateCommand("bpa_apps.pkg_ia.select_department", CommandType.StoredProcedure);
                cmd.BindByName = true;
                DBUtilities.CreateAndAddParameter("p_in_department_name",
                BatchDetail.Department, OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_out_ref_cursor",
                 DBNull.Value, OracleDbType.RefCursor, ParameterDirection.Output,
                 cmd);
              DBUtilities.CreateAndAddParameter("p_out_result",
                 OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
              DBUtilities.CreateAndAddParameter("p_out_error_message",
                 OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);

              using (OracleDataReader dataReader = cmd.ExecuteReader())
              {
                  if (cmd.Parameters["p_out_result"].Value.ToString()
                     .ToUpper() != "SUCCESSFUL")
                  {
                      throw new Exception("-266088529; Procedure Error: " +
                         cmd.Parameters["p_out_result"].Value.ToString() +
                         "; Oracle Error: " + cmd.Parameters[
                         "p_out_error_message"].Value.ToString());
                  }
                  else
                  {
                      if (dataReader.HasRows)
                      {
                          DataTable dt = new DataTable("Results");
                          DataSetResults.Tables.Add(dt);
                          DataSetResults.Load(dataReader, LoadOption.PreserveChanges, DataSetResults.Tables[0]);
                          Disconnect();
                          return DataSetResults;
                      }
                      else
                      {
                          Disconnect();
                          return null;
                      }
                  }
              }
          }
          catch (Exception ex)
          {
              throw new Exception("DataHandler.DataAccess.getDepartmentDetails: " + ex.Message);
          }
      }
      public List<string> getDepartmentList()
      {
         try
         {
            List<string> deptList = new List<string>();
            Connect();
            OracleCommand cmd = GenerateCommand("bpa_apps.pkg_ia.select_department_list", CommandType.StoredProcedure);
                cmd.BindByName = true;
                DBUtilities.CreateAndAddParameter("p_out_ref_cursor",
               DBNull.Value, OracleDbType.RefCursor, ParameterDirection.Output,
               cmd);
            DBUtilities.CreateAndAddParameter("p_out_result",
               OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
            DBUtilities.CreateAndAddParameter("p_out_error_message",
               OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);
            using (OracleDataReader dataReader = cmd.ExecuteReader())
            {
               if (cmd.Parameters["p_out_result"].Value.ToString()
                  .ToUpper() != "SUCCESSFUL")
               {
                  throw new Exception("-266088529; Procedure Error: " +
                     cmd.Parameters["p_out_result"].Value.ToString() +
                     "; Oracle Error: " + cmd.Parameters[
                     "p_out_error_message"].Value.ToString());
               }
               else
               {
                  // Always call Read before accessing data.
                  while (dataReader.Read())
                  {
                     deptList.Add(dataReader.GetString(0));
                  }
               }
            }
            Disconnect();
            return deptList;
         }
         catch (Exception ex)
         {
            throw new Exception("DataHandler.DataAccess.getDepartmentList: " + ex.Message);
         }

      }
      public Dictionary<int, string> getRescanReasons()
      {
         try
         {
            Dictionary<int, string> rescanReasons = new Dictionary<int, string>();
            Connect();
            OracleCommand cmd = GenerateCommand("bpa_apps.pkg_ia.select_rescan_codes", CommandType.StoredProcedure);
                cmd.BindByName = true;
                DBUtilities.CreateAndAddParameter("p_out_ref_cursor",
               DBNull.Value, OracleDbType.RefCursor, ParameterDirection.Output,
               cmd);
            DBUtilities.CreateAndAddParameter("p_out_result",
               OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
            DBUtilities.CreateAndAddParameter("p_out_error_message",
               OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);
            using (OracleDataReader dataReader = cmd.ExecuteReader())
            {
               if (cmd.Parameters["p_out_result"].Value.ToString()
                  .ToUpper() != "SUCCESSFUL")
               {
                  throw new Exception("-266088529; Procedure Error: " +
                     cmd.Parameters["p_out_result"].Value.ToString() +
                     "; Oracle Error: " + cmd.Parameters[
                     "p_out_error_message"].Value.ToString());
               }
               else
               {
                  // Always call Read before accessing data.
                  while (dataReader.Read())
                  {
                     rescanReasons.Add(dataReader.GetInt32(0), dataReader.GetString(1));
                  }
               }
            }
            Disconnect();
            return rescanReasons;
         }
         catch (Exception ex)
         {
             throw new Exception("DataHandler.DataAccess.getRescanReasons: " + ex.Message);
         }

      }
      public string getScannerID()
      {

          try
          {
              string scannerID = string.Empty;
              Connect();
              OracleCommand cmd = GenerateCommand("bpa_apps.pkg_ia.select_scanner_id", CommandType.StoredProcedure);
                cmd.BindByName = true;
                DBUtilities.CreateAndAddParameter("p_in_machine_name",
                  System.Environment.MachineName.ToString(), OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_out_scanner_id",
                OracleDbType.Varchar2, ParameterDirection.Output, 15, cmd);
              DBUtilities.CreateAndAddParameter("p_out_result",
                 OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
              DBUtilities.CreateAndAddParameter("p_out_error_message",
                 OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);

              cmd.ExecuteNonQuery();

              if (cmd.Parameters["p_out_result"].Value.ToString().ToUpper() == "SUCCESSFUL")
              {
                  //grab the value
                  scannerID = cmd.Parameters["p_out_scanner_id"]
                     .Value.ToString();
              }
              else
              {
                  throw new Exception("-266088529; Procedure Error: " +
                     cmd.Parameters["p_out_result"].Value.ToString() + "; Oracle Error: " +
                     cmd.Parameters["p_out_error_message"].Value.ToString());
              }
              Disconnect();
              return scannerID;
          }
          catch (Exception ex)
          {
              throw new Exception("DataHandler.DataAccess.getScannerID: " + ex.Message);
          }
      }
      public List<string> getUserList()
      {
          try
          {
              List<string> deptList = new List<string>();
              Connect();
              OracleCommand cmd = GenerateCommand("bpa_apps.pkg_ia.select_prep_users", CommandType.StoredProcedure);
                cmd.BindByName = true;
                DBUtilities.CreateAndAddParameter("p_in_site_id",
                 BatchDetail.SiteID, OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_out_ref_cursor",
                 DBNull.Value, OracleDbType.RefCursor, ParameterDirection.Output,
                 cmd);
              DBUtilities.CreateAndAddParameter("p_out_result",
                 OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
              DBUtilities.CreateAndAddParameter("p_out_error_message",
                 OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);
              using (OracleDataReader dataReader = cmd.ExecuteReader())
              {
                  if (cmd.Parameters["p_out_result"].Value.ToString()
                     .ToUpper() != "SUCCESSFUL")
                  {
                      throw new Exception("-266088529; Procedure Error: " +
                         cmd.Parameters["p_out_result"].Value.ToString() +
                         "; Oracle Error: " + cmd.Parameters[
                         "p_out_error_message"].Value.ToString());
                  }
                  else
                  {
                      // Always call Read before accessing data.
                      while (dataReader.Read())
                      {
                          string addUser = string.Empty;
                          if (!dataReader.IsDBNull(1))
                          {
                              addUser = dataReader.GetString(1);
                          }
                          if (!dataReader.IsDBNull(2))
                          {
                              addUser += ", " + dataReader.GetString(2);
                          }
                          addUser += " - " + dataReader.GetString(0);
                          deptList.Add(addUser);                          
                      }
                  }
              }
              Disconnect();
              return deptList;
          }
          catch (Exception ex)
          {
              throw new Exception("DataHandler.DataAccess.getDepartmentList: " + ex.Message);
          }

      }
      public Dictionary<string, string> getPrepIssueTypes()
      {          
          try
          {
              Dictionary<string, string> prepErrors = new Dictionary<string, string>();
              Connect();
              OracleCommand cmd = GenerateCommand("bpa_apps.pkg_ia.select_issue_types", CommandType.StoredProcedure);
                cmd.BindByName = true;
              DBUtilities.CreateAndAddParameter("p_out_ref_cursor",
                 DBNull.Value, OracleDbType.RefCursor, ParameterDirection.Output,
                 cmd);
              DBUtilities.CreateAndAddParameter("p_out_result",
                 OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
              DBUtilities.CreateAndAddParameter("p_out_error_message",
                 OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);
              using (OracleDataReader dataReader = cmd.ExecuteReader())
              {
                  if (cmd.Parameters["p_out_result"].Value.ToString()
                     .ToUpper() != "SUCCESSFUL")
                  {
                      throw new Exception("-266088529; Procedure Error: " +
                         cmd.Parameters["p_out_result"].Value.ToString() +
                         "; Oracle Error: " + cmd.Parameters[
                         "p_out_error_message"].Value.ToString());
                  }
                  else
                  {
                      // Always call Read before accessing data.
                      while (dataReader.Read())
                      {
                         prepErrors.Add(dataReader.GetString(0), "0");                          
                      }
                  }
              }
              Disconnect();
              return prepErrors;
          }
          catch (Exception ex)
          {
              throw new Exception("DataHandler.DataAccess.getPrepErrorTypes: " + ex.Message);
          }
      }
      public void insertBatchIssue(string item, Int32 frequency)
      {
          try
          {
              Connect();
              OracleCommand cmd = GenerateCommand("bpa_apps.pkg_batch.create_batch_issues", CommandType.StoredProcedure);
                cmd.BindByName = true;
                DBUtilities.CreateAndAddParameter("p_in_app_name",
                 "ScanPlus", OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_in_machine_name",
                 System.Environment.MachineName.ToUpper(), OracleDbType.Varchar2, ParameterDirection.Input, cmd); 
              DBUtilities.CreateAndAddParameter("p_in_user_id",
                 System.Environment.UserName.ToUpper(), OracleDbType.Varchar2, ParameterDirection.Input, cmd);            
              DBUtilities.CreateAndAddParameter("p_in_batch_no",
                 BatchDetail.BatchNo, OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_in_issue_type",
                 item, OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_in_frequency",
                 frequency, OracleDbType.Int32, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_out_result",
                 OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
              DBUtilities.CreateAndAddParameter("p_out_error_message",
                 OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);

              cmd.ExecuteNonQuery();

              if (cmd.Parameters["p_out_result"].Value.ToString().ToUpper() != "SUCCESSFUL")
              {
                  throw new Exception("-266088529; Procedure Error: " +
                    cmd.Parameters["p_out_result"].Value.ToString() + "; Oracle Error: " +
                    cmd.Parameters["p_out_error_message"].Value.ToString());
              }
              Disconnect();
              
          }
          catch (Exception ex)
          {
              throw new Exception("DataHandler.DataAccess.insertBatchIssue: " + ex.Message);
          } 
      }
      public void insertDepartmentAssignment(string ProcessName, string Department)
      {
          try
          {
              Connect();
              OracleCommand cmd = GenerateCommand("bpa_apps.pkg_ia.insert_assigned_departments", CommandType.StoredProcedure);
                cmd.BindByName = true;
                DBUtilities.CreateAndAddParameter("p_in_job_name",
                 ProcessName, OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_in_department_list",
                 Department, OracleDbType.Varchar2, ParameterDirection.Input, cmd);
              DBUtilities.CreateAndAddParameter("p_out_result",
                 OracleDbType.Varchar2, ParameterDirection.Output, 255, cmd);
              DBUtilities.CreateAndAddParameter("p_out_error_message",
                 OracleDbType.Varchar2, ParameterDirection.Output, 4000, cmd);

              cmd.ExecuteNonQuery();

              if (cmd.Parameters["p_out_result"].Value.ToString().ToUpper() != "SUCCESSFUL")
              {
                  throw new Exception("-266088529; Procedure Error: " +
                    cmd.Parameters["p_out_result"].Value.ToString() + "; Oracle Error: " +
                    cmd.Parameters["p_out_error_message"].Value.ToString());
              }
              Disconnect();

          }
          catch (Exception ex)
          {
              throw new Exception("DataHandler.DataAccess.insertDepartmentAssignment: " + ex.Message);
          } 
      }
      #endregion

      #region IDisposable Members

      public void Dispose()
      {
         crypto = null;
         _parmsCustom = null;
         _connection = null;
         _connectionString = null;
         _transaction = null;
         _DSN = string.Empty;
         _DBUser = string.Empty;
         _DBPass = string.Empty;
      }

      #endregion


   }

}
