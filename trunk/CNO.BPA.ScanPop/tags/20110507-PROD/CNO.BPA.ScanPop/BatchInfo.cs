using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CNO.BPA.ScanPop
{   
    public partial class BatchInfo : Form
    {
        #region Variables
        CustomParameters _parmsCustom = null;
        DataHandler.DataAccess _dbAccess = null;
        List<string> _userList = null;
        #endregion
        #region Declarations
        public BatchInfo(ref CustomParameters ParmsCustom)
        {
            InitializeComponent();
            _parmsCustom = ParmsCustom;
            _dbAccess = new CNO.BPA.ScanPop.DataHandler.DataAccess(ref _parmsCustom);

        }
        #endregion
        #region Private Methods
        private void getDepartmentSpecifics()
        {
            DataSet datasetResults = _dbAccess.getDepartmentDetails();
            DataRow dataRow = datasetResults.Tables[0].Rows[0];

            BatchDetail.BatchPriority = dataRow.ItemArray.GetValue(1).ToString();   //priority
            BatchDetail.WorkCategory = dataRow.ItemArray.GetValue(2).ToString();   //work category         
            BatchDetail.MDWMaxDocCount = dataRow.ItemArray.GetValue(3).ToString();   //mdw max doc count    
            BatchDetail.MaxClaimsPerDoc = dataRow.ItemArray.GetValue(4).ToString();   //max claims per doc
            BatchDetail.BatchAgeing = dataRow.ItemArray.GetValue(5).ToString();   //batch ageing
            BatchDetail.TrackUser = dataRow.ItemArray.GetValue(6).ToString();   //track user
            BatchDetail.TrackPerformance = dataRow.ItemArray.GetValue(7).ToString();   //track performance
            BatchDetail.PriorityFactor = dataRow.ItemArray.GetValue(8).ToString();   //priority factor
        }
        private bool formValidate()
        {


            //first validate all fields are complete
            if (lstDepartment.SelectedItem != null)
            {
                BatchDetail.Department = lstDepartment.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("A department must be selected in order to continue.");
                return false;
            }
            if (dtpReceivedDate.Value.Date < DateTime.Now.Date.Subtract(TimeSpan.FromDays(7)))
            {
                DialogResult result = MessageBox.Show(
                    "You have selected a Received Date that is more than 7 days ago,"
                    + "\r\nare you sure this is correct?", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    BatchDetail.ReceivedDate = dtpReceivedDate.Value.ToString("MM/dd/yyyy HH:mm:ss");
                }
            }
            else if (dtpReceivedDate.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show(
                    "You have selected a Receive Date that is in the future."
                    + "\r\nPlease select the Receive Date again.", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                BatchDetail.ReceivedDate = dtpReceivedDate.Value.ToString("MM/dd/yyyy HH:mm:ss");
            }
            if (dtpReceivedDate.Value.Date > dtpRecordsDate.Value.Date)
            {
                MessageBox.Show(
                    "You have selected a Records Date that occurs prior to the received date."
                    + "\r\nPlease select the Records Date again.", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (dtpRecordsDate.Value.Date < DateTime.Now.Date.Subtract(TimeSpan.FromDays(7)))
            {
                DialogResult result = MessageBox.Show(
                    "You have selected a Records Date that is more than 7 days ago,"
                    + "\r\nare you sure this is correct?", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    BatchDetail.ReceivedDateCRD = dtpRecordsDate.Value.ToString("MM/dd/yyyy HH:mm:ss");
                }
            }
            else if (dtpRecordsDate.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show(
                    "You have selected a Records Date that is in the future."
                    + "\r\nPlease select the Records Date again.", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                BatchDetail.ReceivedDateCRD = dtpRecordsDate.Value.ToString("MM/dd/yyyy HH:mm:ss");
            }
            if (dtpRecordsDate.Value.Date > dtpPrepDate.Value.Date)
            {
                MessageBox.Show(
                    "You have selected a Prep Date that occurs prior to the Records date."
                    + "\r\nPlease select the Prep Date again.", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (dtpPrepDate.Value.Date < DateTime.Now.Date.Subtract(TimeSpan.FromDays(7)))
            {
                DialogResult result = MessageBox.Show(
                    "You have selected a Prep Date that is more than 7 days ago,"
                    + "\r\nare you sure this is correct?", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.No)
                {
                    return false;
                }
                else
                {
                    BatchDetail.PrepDate = dtpPrepDate.Value.ToString("MM/dd/yyyy");
                }
            }
            else if (dtpReceivedDate.Value.Date > dtpPrepDate.Value.Date)
            {
                MessageBox.Show(
                    "You have selected a Prep Date that occurs prior to the Received date."
                    + "\r\nPlease select the Prep Date again.", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (dtpPrepDate.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show(
                    "You have selected a Prep Date that is in the future."
                    + "\r\nPlease select the Prep Date again.", "DATE RANGE EXCEPTION",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                BatchDetail.PrepDate = dtpPrepDate.Value.ToString("MM/dd/yyyy");
            }
            if (lstPrepOperator.SelectedItem != null)
            {
                BatchDetail.PrepOperator = lstPrepOperator.SelectedItem.ToString().Substring((lstPrepOperator.SelectedItem.ToString().IndexOf("-") + 1)).Trim();
            }
            else
            {
                MessageBox.Show("A prep operator must be selected in order to continue.");
                return false;
            }
            if (txtBoxNumber.Text.Length > 0)
            {
                BatchDetail.BoxNo = DateTime.Now.ToString("MMddyy") +
                    txtBoxNumber.Text.PadLeft(3, Convert.ToChar("0"));
            }
            else
            {
                MessageBox.Show("A box number must be entered in order to continue.");
                return false;
            }
            if (chkRescan.Checked)
            {
                //the original batch number must be filled out and also the rescan reason selected
                if (txtOriginalBatchNo.Text.Length > 0 & lstRescanReason.SelectedItem != null)
                {
                    if (txtOriginalBatchNo.Text.Length == 14)
                    {
                        BatchDetail.Rescan = lstRescanReason.SelectedValue.ToString();
                        BatchDetail.OriginalBatchNo = txtOriginalBatchNo.Text;
                    }
                    else
                    {
                        MessageBox.Show("The original batch number must be 14 characters.");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Both the original batch number and a rescan reason\r\n are required in order to continue.");
                    return false;
                }
            }

            if (!rbYes.Checked & !rbNo.Checked)
            {
                MessageBox.Show("Please indicate whether or not there were any prep issues.");
                return false;
            }

            return true;
        }
        #endregion

        private void BatchInfo_Shown(object sender, EventArgs e)
        {
              //set the max dates to today
            dtpReceivedDate.Value = DateTime.Now;
            dtpRecordsDate.Value = DateTime.Now;
            dtpPrepDate.Value = DateTime.Now;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //first we need to validate all of the fields on the form
            if (!formValidate())
            {
                //if there is something missing do not accept the form
                return;
            }
            //if the other fields are valid, go ahead and get the department data
            getDepartmentSpecifics();

            //then leave
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkRescan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRescan.Checked)
            {
                Dictionary<int, string> reasons = _dbAccess.getRescanReasons();
                lstRescanReason.DataSource = new BindingSource(reasons, null);
                lstRescanReason.DisplayMember = "Value";
                lstRescanReason.ValueMember = "Key";                
                txtOriginalBatchNo.Enabled = true;
                txtOriginalBatchNo.BackColor = Color.White;
                lstRescanReason.Enabled = true;
                lstRescanReason.BackColor = Color.White;
            }
            else
            {
                lstRescanReason.Items.Clear();
                txtOriginalBatchNo.Text = "";
                txtOriginalBatchNo.Enabled = false;
                txtOriginalBatchNo.BackColor = SystemColors.ControlDark;
                lstRescanReason.SelectedIndex = -1;
                lstRescanReason.Enabled = false;
                lstRescanReason.BackColor = SystemColors.ControlDark;
            }
        }

        private void dtpPrepDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpPrepDate.Value.Date < DateTime.Now.Date.Subtract(TimeSpan.FromDays(7))
                || dtpRecordsDate.Value.Date > dtpPrepDate.Value.Date
                || dtpPrepDate.Value.Date > DateTime.Now.Date)
            {
                dtpPrepDate.ForeColor = Color.Red;
                lblErrorText.Text = "DATE RANGE EXCEPTION";
            }
            else
            {
                dtpPrepDate.ForeColor = Color.Black;
                lblErrorText.Text = string.Empty;
            }
        }

        private void dtpReceivedDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpReceivedDate.Value.Date < DateTime.Now.Date.Subtract(TimeSpan.FromDays(7))
                || dtpReceivedDate.Value.Date > DateTime.Now.Date)
            {
                dtpReceivedDate.ForeColor = Color.Red;
                lblErrorText.Text = "DATE RANGE EXCEPTION";
            }
            else
            {
                dtpReceivedDate.ForeColor = Color.Black;
                lblErrorText.Text = string.Empty;
            }
            dtpRecordsDate.Value = dtpReceivedDate.Value;
        }

        private void dtpRecordsDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpRecordsDate.Value.Date < DateTime.Now.Date.Subtract(TimeSpan.FromDays(7))
                || dtpReceivedDate.Value.Date > dtpRecordsDate.Value.Date 
                || dtpRecordsDate.Value.Date > DateTime.Now.Date)
            {
                dtpRecordsDate.ForeColor = Color.Red;
                lblErrorText.Text = "DATE RANGE EXCEPTION";
            }
            else
            {
                dtpRecordsDate.ForeColor = Color.Black;
                lblErrorText.Text = string.Empty;
            }
        }

        private void lstDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //we need to pull the site id out of the department name
            BatchDetail.SiteID = lstDepartment.SelectedItem.ToString().Substring(0, 3);
            BatchDetail.Department = lstDepartment.SelectedItem.ToString();
            grpDetails.Enabled = true;
            _userList = _dbAccess.getUserList();
        }

        private void lstPrepOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            BatchDetail.PrepOperator = lstPrepOperator.SelectedItem.ToString();
        }

        private void lstPrepOperator_DropDown(object sender, EventArgs e)
        {
            lstPrepOperator.Items.Clear();
            foreach (string user in _userList)
            {
                lstPrepOperator.Items.Add(user);
            }
        }

        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbYes.Checked)
            {
                //both yes and no cannot be checked so make sure that is the case
                rbNo.Checked = false;
                BatchIssues formPrepIssues = new BatchIssues(ref _parmsCustom);
                formPrepIssues.ShowDialog();
                if (formPrepIssues.DialogResult == DialogResult.Cancel)
                {
                    //if the user cancelled, we need to uncheck the button
                    rbYes.Checked = false;
                }
            }             
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNo.Checked)
            {
                rbYes.Checked = false;
            }
        }

        private void txtBoxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtOriginalBatchNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
         
    }
}
