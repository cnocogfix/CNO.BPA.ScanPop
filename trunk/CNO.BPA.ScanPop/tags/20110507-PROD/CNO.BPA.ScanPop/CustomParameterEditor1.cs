using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emc.InputAccel.Workflow.Client;

namespace CNO.BPA.ScanPop
{
    public partial class CustomParameterEditor1 : Form
    {
        CustomParameters _parmsCustom;
        string _processName;

        public CustomParameterEditor1(ref CustomParameters ParmsCustom, string ProcessName)
        {
            InitializeComponent();
            _parmsCustom = ParmsCustom;
            _processName = ProcessName;

            //assume that if the dsn is blank there is nothing we need to decrypt either
            if (_parmsCustom.DSN.Length > 0)
            {
                txtDSN.Text = _parmsCustom.DSN;
                Framework.Cryptography crypto = new Framework.Cryptography();
                txtUserID.Text = crypto.Decrypt(_parmsCustom.UserName);
                txtPassword.Text = crypto.Decrypt(_parmsCustom.Password);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Framework.Cryptography crypto = new Framework.Cryptography();
            _parmsCustom.DSN = txtDSN.Text;
            _parmsCustom.UserName = crypto.Encrypt(txtUserID.Text);
            _parmsCustom.Password = crypto.Encrypt(txtPassword.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSelectDepartments_Click(object sender, EventArgs e)
        {
            CustomDepartmentEditor deptEditor = new CustomDepartmentEditor();  
            //populate the available departments from the db
            if (_parmsCustom.DSN.Length == 0 || _parmsCustom.Password.Length == 0 || _parmsCustom.UserName.Length == 0)
            {
                //user may not have hit apply or filled out the values
                MessageBox.Show("Selecting Departments requires database connection parameters./r/n"
                    + "Please remember to click Apply if this is the first time the values have been entered.");
                return;
            }
            DataHandler.DataAccess dataAccess = new DataHandler.DataAccess(ref _parmsCustom);
            List<string> departments = dataAccess.getDepartmentList();
            foreach (string dept in departments)
            {
                deptEditor.lstAvailableDeptartments.Items.Add(dept);
            }
            //Populte the selected departments list on the editor            
            List<string> assignedDepartments = dataAccess.getDepartmentAssignment(_processName);
            foreach (string dept in assignedDepartments)
            {
                deptEditor.lstSelectedDepartments.Items.Add(dept);
                deptEditor.lstAvailableDeptartments.Items.Remove(dept);
            }

            deptEditor.ShowDialog();
            if (deptEditor.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string departmentList = string.Empty;
                foreach (string dept in deptEditor.lstSelectedDepartments.Items)
                {
                    departmentList += dept + "|";
                }
                //remove the trailing pipe
                departmentList = departmentList.Substring(0, (departmentList.Length - 1));
                //send the update to the database
                dataAccess.insertDepartmentAssignment(_processName, departmentList);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Framework.Cryptography crypto = new Framework.Cryptography();
            _parmsCustom.DSN = txtDSN.Text;
            _parmsCustom.UserName = crypto.Encrypt(txtUserID.Text);
            _parmsCustom.Password = crypto.Encrypt(txtPassword.Text);
        }

    }
}
