using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emc.InputAccel.ScanPlus.Scripting;
using Emc.InputAccel.QuickModule.ClientScriptingInterface;

namespace CNO.BPA.ScanPop
{
    public class ScanPlusTaskEvents : IScanPlusTaskEvents
    {
        public ScriptResult PrepareTask(ITaskEventArg taskInfo, ILevelCreationParameters levelCreateInformation)
        {
            return default(ScriptResult);
        }

        public ScriptResult BeforeScan(ITaskEventArg taskInfo, IScanDriver selectedDriver)
        {
            return default(ScriptResult);
        }

        public ScriptResult AfterScan(ITaskEventArg taskInfo, IScanDriver selectedDriver)
        {
            return default(ScriptResult);
        }

        public void ScanConfigurationSelected(ITaskEventArg taskInfo, IScanDriver selectedDriver)
        {
        }

        public ScriptResult BeforeNewLevel(ITaskEventArg taskInfo, IBatchNode parentNode, IBatchNode previousNode, ILevelCreationParameters levelCreateInformation, ref EventAction action, ref Boolean discard)
        {
            return default(ScriptResult);
        }

        public ScriptResult AfterNewLevel(ITaskEventArg taskInfo, IBatchNode newNode)
        {
            return default(ScriptResult);
        }

        public ScriptResult LevelFinished(ITaskEventArg taskInfo, IBatchNode finishedNode)
        {
            return default(ScriptResult);
        }

        public ScriptResult AfterPageAdded(ITaskEventArg taskInfo, IBatchNode pageNode)
        {
            return default(ScriptResult);
        }

        public ScriptResult BeforeNodeDeleted(ITaskEventArg taskInfo, IBatchNode node)
        {
            return default(ScriptResult);
        }

        public ScriptResult PrepopulateIndexes(ITaskEventArg taskInfo, IBatchNode node, IIndexField[] fields, Boolean initialIndexing)
        {
            return default(ScriptResult);
        }

        public ScriptResult IndexChanged(ITaskEventArg taskInfo, IBatchNode node, IIndexField[] fields, String valueName, Boolean initialIndexing)
        {
            return default(ScriptResult);
        }

        public ScriptResult ValidateIndexes(ITaskEventArg taskInfo, IBatchNode node, IIndexField[] fields, Boolean initialIndexing)
        {
            return default(ScriptResult);
        }

        [CustomParameterType(typeof(CustomParameters))]
        public ScriptResult BeforeBatchClose(ITaskEventArg taskInfo, Boolean autoBatchCreation, ref BatchCloseMode closeMode)
        {
            CustomParameters parmsCustom;
            parmsCustom = (CustomParameters)taskInfo.CustomParameter.Value;
            BatchInfo formBatchInfo = new BatchInfo(ref parmsCustom);
            //generate the batch number and show it to the user
            DataHandler.DataAccess dbAccess = new DataHandler.DataAccess(ref parmsCustom);
            formBatchInfo.lstDepartment.Items.Clear();
            List<string> departmentList = dbAccess.getDepartmentAssignment(
                taskInfo.Application.CurrentStep.Value().GetString("ProcessName", ""));
            foreach (string dept in departmentList)
            {
                formBatchInfo.lstDepartment.Items.Add(dept);
            }
            formBatchInfo.ShowDialog();
            if (formBatchInfo.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                BatchDetail.ScannerID = dbAccess.getScannerID();
                BatchDetail.BatchNo = dbAccess.getBatchNo();
                //send the prep errors to the db
                if (null != BatchDetail.BatchIssues && BatchDetail.BatchIssues.Count > 0)
                {
                    //call insert prep errors for each item where the frequency is not 0
                    foreach (KeyValuePair<string, Int32> issue in BatchDetail.BatchIssues)
                    {
                        if (issue.Value > 0)
                        {
                            dbAccess.insertBatchIssue(issue.Key, issue.Value);
                        }
                    }
                }
                //assign the batch name to the batch
                taskInfo.Application.CurrentStep.BatchProcess.Value().SetString("BatchName",
                    BatchDetail.BatchNo);
                //and now determine and set the priority on the batch
                taskInfo.Application.CurrentStep.BatchProcess.Value().SetInt("Priority",
                    BatchDetail.determineBatchPriority());
                //pop up a window showing the batch number to the user                   
                BatchNotify formBatchCreated = new BatchNotify();
                formBatchCreated.lblBatchNo.Text = BatchDetail.BatchNo;
                formBatchCreated.ShowDialog();
                //setup all 3 manual touchpoints with the department selection and store the batch details
                foreach (IWorkflowStep wfStep in taskInfo.Application.CurrentStep.BatchProcess.WorkflowSteps)
                {
                    switch (wfStep.Name.ToUpper())
                    {
                        case "AUTOCLASSIFY":
                            {
                                taskInfo.Application.CurrentStep.BatchProcess.Value().SetString("$batch=" +
                                    taskInfo.Application.CurrentStep.BatchProcess.Id + "/AutoClassify.IADepartments", 
                                    BatchDetail.Department);
                                break;
                            }
                        case "AUTOVALIDATION":
                            {
                                taskInfo.Application.CurrentStep.BatchProcess.Value().SetString("$batch=" +
                                    taskInfo.Application.CurrentStep.BatchProcess.Id + "/AutoValidation.IADepartments", 
                                    BatchDetail.Department);
                                break;
                            }
                        case "MANUALCLASSIFY":
                            {
                                taskInfo.Application.CurrentStep.BatchProcess.Value().SetString("$batch=" +
                                    taskInfo.Application.CurrentStep.BatchProcess.Id + "/ManualClassify.IADepartments",
                                    BatchDetail.Department);
                                break;
                            }
                        case "MANUALVAL":
                            {
                                taskInfo.Application.CurrentStep.BatchProcess.Value().SetString("$batch=" +
                                    taskInfo.Application.CurrentStep.BatchProcess.Id + "/ManualVal.IADepartments", 
                                    BatchDetail.Department);
                                break;
                            }
                        case "MANUALINDEX":
                            {
                                taskInfo.Application.CurrentStep.BatchProcess.Value().SetString("$batch=" +
                                    taskInfo.Application.CurrentStep.BatchProcess.Id + "/ManualIndex.IADepartments", 
                                    BatchDetail.Department);
                                break;
                            }
                        case "STANDARD_MDF":
                            {
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("BATCH_NO", BatchDetail.BatchNo);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("BATCH_DEPARTMENT", BatchDetail.Department);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("BATCH_PRIORITY", BatchDetail.BatchPriority);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("SITE_ID", BatchDetail.SiteID);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("WORK_CATEGORY", BatchDetail.WorkCategory);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("BOX_NO", BatchDetail.BoxNo);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("PREP_OPERATOR", BatchDetail.PrepOperator);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("PREP_DATE", BatchDetail.PrepDate);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("SCANNER_ID", BatchDetail.ScannerID);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("SCAN_MACHINE", System.Environment.MachineName.ToString());
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("SCAN_OPERATOR", System.Environment.UserName.ToString());                                
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("RECEIVED_DATE", BatchDetail.ReceivedDate);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("CRD_RECEIVED_DATE", BatchDetail.ReceivedDateCRD);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("BATCH_AGEING", BatchDetail.BatchAgeing);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("MAX_CLAIMS_PER_DOC", BatchDetail.MaxClaimsPerDoc);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("TRACK_USER", BatchDetail.TrackUser);
                                taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("TRACK_PERFORMANCE", BatchDetail.TrackPerformance);                                
                                if (BatchDetail.Rescan.Length > 0)
                                {
                                    taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("RESCAN_CHECKED", "1");
                                    taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("ORIGINAL_BATCH_NO", BatchDetail.OriginalBatchNo);
                                    taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("RESCAN_REASON", BatchDetail.Rescan);
                                }
                                //setup the dispatcher project if exists
                                if (BatchDetail.DispatcherProject.Length != 0)
                                {
                                    //start by getting both the citrix and server paths
                                    //string dispCitrixPath = taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).GetString("DISP_CITRIX_PATH", "");
                                    //string dispServerPath = taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).GetString("DISP_SERVER_PATH", "");
                                    //now set the final values
                                    taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("DISP_PROJECT", BatchDetail.DispatcherProject);
                                    //taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("DISP_CITRIX_PATH", dispCitrixPath + "\\" + BatchDetail.DispatcherProject);
                                    //taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep).SetString("DISP_SERVER_PATH", dispServerPath + "\\" + BatchDetail.DispatcherProject);

                                }
                                break;
                            }
                    }
                }
            }
            else
            {
                System.Windows.Forms.DialogResult drResult = System.Windows.Forms.MessageBox.Show(
                    "Would you like to cancel the batch at this time?", "USER VERIFICATION NEEDED",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (drResult == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (IWorkflowStep wfStep in taskInfo.Application
                        .CurrentStep.BatchProcess.WorkflowSteps)
                    {
                        if (wfStep.Name.ToUpper() == "STANDARD_MDF")
                        {
                            taskInfo.Application.CurrentStep.BatchProcess.Tree.Values(wfStep)
                                .SetString("BATCH_STATUS", "CANCEL");
                        }
                    }
                }
                else
                {
                    return ScriptResult.Cancel;
                }
            }
            //regardless of ok or cancel clear out any batch details just to be safe
            BatchDetail.Clear();
            return default(ScriptResult);
        }

        public ScriptResult AutoBatchCreation(ITaskEventArg taskInfo, INewBatchParameters batchParams)
        {
            return default(ScriptResult);
        }

        public Boolean FilterProcessList(ITaskEventArg taskInfo, IBatchInformation process)
        {
            return true;
        }

    }
}
