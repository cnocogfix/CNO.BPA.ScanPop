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
    public partial class BatchIssues : Form
    {
        CustomParameters _parmsCustom = null;
        DataHandler.DataAccess _dbAccess = null;
        Dictionary<string, string> _prepIssues = null;

        public BatchIssues(ref CustomParameters ParmsCustom)
        {
            InitializeComponent();
            _parmsCustom = ParmsCustom;
            _dbAccess = new CNO.BPA.ScanPop.DataHandler.DataAccess(ref _parmsCustom);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if they click ok, establish a new dictionary
            BatchDetail.BatchIssues = new Dictionary<string,Int32>();              
            foreach (DataGridViewRow row in dgvPrepErrors.Rows)
            {
                if (row.Cells[1].Value.ToString() != "0" && row.Cells[1].Value.ToString().Length > 0 )
                {
                    //check to be sure the value is a number;
                    int frequency;
                    int.TryParse(row.Cells[1].Value.ToString(),out frequency);
                    if (frequency == 0)
                    {
                        MessageBox.Show("Frequency values must be numeric", "USER ACTION REQUIRED");
                        return;
                    }
                    else
                    {
                        BatchDetail.BatchIssues.Add(row.Cells[0].Value.ToString(), frequency);
                    }
                }
            }
            if (BatchDetail.BatchIssues.Count == 0)
            {
                MessageBox.Show("You must indicate the number of issues encountered for the current batch");
                return;
            }
            //then leave
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void populateGrid()
        {
            _prepIssues = _dbAccess.getPrepIssueTypes();
            string[] dbValues = new string[2];
            foreach (string item in _prepIssues.Keys)
            {
                string value = string.Empty;
                _prepIssues.TryGetValue(item, out value);
                dbValues[0] = item;
                dbValues[1] = value;
                dgvPrepErrors.Rows.Add(dbValues);
            }


        }

        private void PrepIssues_Shown(object sender, EventArgs e)
        {
            populateGrid();
        }

    }
}
