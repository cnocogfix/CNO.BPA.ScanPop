namespace CNO.BPA.ScanPop
{
    partial class BatchInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpSiteDept = new System.Windows.Forms.GroupBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lstDepartment = new System.Windows.Forms.ComboBox();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.dtpReceivedDate = new System.Windows.Forms.DateTimePicker();
            this.rbNo = new System.Windows.Forms.RadioButton();
            this.rbYes = new System.Windows.Forms.RadioButton();
            this.lblPreppingIssue = new System.Windows.Forms.Label();
            this.lstRescanReason = new System.Windows.Forms.ComboBox();
            this.lblRescanReason = new System.Windows.Forms.Label();
            this.chkRescan = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOriginalBatchNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxNumber = new System.Windows.Forms.TextBox();
            this.lblPrepOperator = new System.Windows.Forms.Label();
            this.lstPrepOperator = new System.Windows.Forms.ComboBox();
            this.lblPrepDate = new System.Windows.Forms.Label();
            this.dtpPrepDate = new System.Windows.Forms.DateTimePicker();
            this.lblReceivedDate = new System.Windows.Forms.Label();
            this.lblRecordsDate = new System.Windows.Forms.Label();
            this.dtpRecordsDate = new System.Windows.Forms.DateTimePicker();
            this.pnlReceivedDate = new System.Windows.Forms.Panel();
            this.pnlPrepDate = new System.Windows.Forms.Panel();
            this.pnlRecordsDate = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblErrorText = new System.Windows.Forms.Label();
            this.grpSiteDept.SuspendLayout();
            this.grpDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSiteDept
            // 
            this.grpSiteDept.Controls.Add(this.lblDepartment);
            this.grpSiteDept.Controls.Add(this.lstDepartment);
            this.grpSiteDept.Location = new System.Drawing.Point(12, 6);
            this.grpSiteDept.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpSiteDept.Name = "grpSiteDept";
            this.grpSiteDept.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpSiteDept.Size = new System.Drawing.Size(472, 80);
            this.grpSiteDept.TabIndex = 0;
            this.grpSiteDept.TabStop = false;
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Location = new System.Drawing.Point(68, 31);
            this.lblDepartment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(113, 20);
            this.lblDepartment.TabIndex = 3;
            this.lblDepartment.Text = "Department:";
            this.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstDepartment
            // 
            this.lstDepartment.DropDownHeight = 120;
            this.lstDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstDepartment.DropDownWidth = 200;
            this.lstDepartment.FormattingEnabled = true;
            this.lstDepartment.IntegralHeight = false;
            this.lstDepartment.Location = new System.Drawing.Point(190, 26);
            this.lstDepartment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstDepartment.Name = "lstDepartment";
            this.lstDepartment.Size = new System.Drawing.Size(271, 28);
            this.lstDepartment.Sorted = true;
            this.lstDepartment.TabIndex = 1;
            this.lstDepartment.SelectedIndexChanged += new System.EventHandler(this.lstDepartment_SelectedIndexChanged);
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.dtpReceivedDate);
            this.grpDetails.Controls.Add(this.rbNo);
            this.grpDetails.Controls.Add(this.rbYes);
            this.grpDetails.Controls.Add(this.lblPreppingIssue);
            this.grpDetails.Controls.Add(this.lstRescanReason);
            this.grpDetails.Controls.Add(this.lblRescanReason);
            this.grpDetails.Controls.Add(this.chkRescan);
            this.grpDetails.Controls.Add(this.label3);
            this.grpDetails.Controls.Add(this.txtOriginalBatchNo);
            this.grpDetails.Controls.Add(this.label1);
            this.grpDetails.Controls.Add(this.txtBoxNumber);
            this.grpDetails.Controls.Add(this.lblPrepOperator);
            this.grpDetails.Controls.Add(this.lstPrepOperator);
            this.grpDetails.Controls.Add(this.lblPrepDate);
            this.grpDetails.Controls.Add(this.dtpPrepDate);
            this.grpDetails.Controls.Add(this.lblReceivedDate);
            this.grpDetails.Controls.Add(this.lblRecordsDate);
            this.grpDetails.Controls.Add(this.dtpRecordsDate);
            this.grpDetails.Controls.Add(this.pnlReceivedDate);
            this.grpDetails.Controls.Add(this.pnlPrepDate);
            this.grpDetails.Controls.Add(this.pnlRecordsDate);
            this.grpDetails.Enabled = false;
            this.grpDetails.Location = new System.Drawing.Point(12, 86);
            this.grpDetails.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDetails.Size = new System.Drawing.Size(472, 345);
            this.grpDetails.TabIndex = 1;
            this.grpDetails.TabStop = false;
            // 
            // dtpReceivedDate
            // 
            this.dtpReceivedDate.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dtpReceivedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReceivedDate.Location = new System.Drawing.Point(190, 26);
            this.dtpReceivedDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpReceivedDate.Name = "dtpReceivedDate";
            this.dtpReceivedDate.Size = new System.Drawing.Size(265, 26);
            this.dtpReceivedDate.TabIndex = 5;
            this.dtpReceivedDate.ValueChanged += new System.EventHandler(this.dtpReceivedDate_ValueChanged);
            // 
            // rbNo
            // 
            this.rbNo.AutoSize = true;
            this.rbNo.Location = new System.Drawing.Point(264, 306);
            this.rbNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbNo.Name = "rbNo";
            this.rbNo.Size = new System.Drawing.Size(54, 24);
            this.rbNo.TabIndex = 22;
            this.rbNo.TabStop = true;
            this.rbNo.Text = "No";
            this.rbNo.UseVisualStyleBackColor = true;
            this.rbNo.CheckedChanged += new System.EventHandler(this.rbNo_CheckedChanged);
            // 
            // rbYes
            // 
            this.rbYes.AutoSize = true;
            this.rbYes.Location = new System.Drawing.Point(190, 306);
            this.rbYes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbYes.Name = "rbYes";
            this.rbYes.Size = new System.Drawing.Size(62, 24);
            this.rbYes.TabIndex = 21;
            this.rbYes.TabStop = true;
            this.rbYes.Text = "Yes";
            this.rbYes.UseVisualStyleBackColor = true;
            this.rbYes.CheckedChanged += new System.EventHandler(this.rbYes_CheckedChanged);
            // 
            // lblPreppingIssue
            // 
            this.lblPreppingIssue.AutoSize = true;
            this.lblPreppingIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreppingIssue.Location = new System.Drawing.Point(39, 309);
            this.lblPreppingIssue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPreppingIssue.Name = "lblPreppingIssue";
            this.lblPreppingIssue.Size = new System.Drawing.Size(140, 20);
            this.lblPreppingIssue.TabIndex = 20;
            this.lblPreppingIssue.Text = "Prepping Issue:";
            this.lblPreppingIssue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstRescanReason
            // 
            this.lstRescanReason.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lstRescanReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstRescanReason.Enabled = false;
            this.lstRescanReason.FormattingEnabled = true;
            this.lstRescanReason.Location = new System.Drawing.Point(190, 266);
            this.lstRescanReason.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstRescanReason.Name = "lstRescanReason";
            this.lstRescanReason.Size = new System.Drawing.Size(265, 28);
            this.lstRescanReason.TabIndex = 18;
            // 
            // lblRescanReason
            // 
            this.lblRescanReason.AutoSize = true;
            this.lblRescanReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRescanReason.Location = new System.Drawing.Point(30, 271);
            this.lblRescanReason.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRescanReason.Name = "lblRescanReason";
            this.lblRescanReason.Size = new System.Drawing.Size(147, 20);
            this.lblRescanReason.TabIndex = 14;
            this.lblRescanReason.Text = "Rescan Reason:";
            this.lblRescanReason.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkRescan
            // 
            this.chkRescan.AutoSize = true;
            this.chkRescan.Location = new System.Drawing.Point(190, 231);
            this.chkRescan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkRescan.Name = "chkRescan";
            this.chkRescan.Size = new System.Drawing.Size(22, 21);
            this.chkRescan.TabIndex = 17;
            this.chkRescan.UseVisualStyleBackColor = true;
            this.chkRescan.CheckedChanged += new System.EventHandler(this.chkRescan_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 231);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Check If Rescan:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOriginalBatchNo
            // 
            this.txtOriginalBatchNo.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtOriginalBatchNo.Enabled = false;
            this.txtOriginalBatchNo.Location = new System.Drawing.Point(222, 226);
            this.txtOriginalBatchNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOriginalBatchNo.MaxLength = 14;
            this.txtOriginalBatchNo.Name = "txtOriginalBatchNo";
            this.txtOriginalBatchNo.Size = new System.Drawing.Size(234, 26);
            this.txtOriginalBatchNo.TabIndex = 15;
            this.txtOriginalBatchNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOriginalBatchNo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 192);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Box Number:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxNumber
            // 
            this.txtBoxNumber.Location = new System.Drawing.Point(190, 188);
            this.txtBoxNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxNumber.MaxLength = 3;
            this.txtBoxNumber.Name = "txtBoxNumber";
            this.txtBoxNumber.Size = new System.Drawing.Size(265, 26);
            this.txtBoxNumber.TabIndex = 11;
            this.txtBoxNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxNumber_KeyPress);
            // 
            // lblPrepOperator
            // 
            this.lblPrepOperator.AutoSize = true;
            this.lblPrepOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrepOperator.Location = new System.Drawing.Point(46, 154);
            this.lblPrepOperator.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrepOperator.Name = "lblPrepOperator";
            this.lblPrepOperator.Size = new System.Drawing.Size(134, 20);
            this.lblPrepOperator.TabIndex = 10;
            this.lblPrepOperator.Text = "Prep Operator:";
            this.lblPrepOperator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstPrepOperator
            // 
            this.lstPrepOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstPrepOperator.FormattingEnabled = true;
            this.lstPrepOperator.Location = new System.Drawing.Point(190, 149);
            this.lstPrepOperator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lstPrepOperator.Name = "lstPrepOperator";
            this.lstPrepOperator.Size = new System.Drawing.Size(265, 28);
            this.lstPrepOperator.Sorted = true;
            this.lstPrepOperator.TabIndex = 9;
            this.lstPrepOperator.DropDown += new System.EventHandler(this.lstPrepOperator_DropDown);
            this.lstPrepOperator.SelectedIndexChanged += new System.EventHandler(this.lstPrepOperator_SelectedIndexChanged);
            // 
            // lblPrepDate
            // 
            this.lblPrepDate.AutoSize = true;
            this.lblPrepDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrepDate.Location = new System.Drawing.Point(80, 115);
            this.lblPrepDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrepDate.Name = "lblPrepDate";
            this.lblPrepDate.Size = new System.Drawing.Size(100, 20);
            this.lblPrepDate.TabIndex = 8;
            this.lblPrepDate.Text = "Prep Date:";
            this.lblPrepDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpPrepDate
            // 
            this.dtpPrepDate.CustomFormat = "MM/dd/yyyy";
            this.dtpPrepDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrepDate.Location = new System.Drawing.Point(190, 109);
            this.dtpPrepDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpPrepDate.Name = "dtpPrepDate";
            this.dtpPrepDate.Size = new System.Drawing.Size(265, 26);
            this.dtpPrepDate.TabIndex = 7;
            this.dtpPrepDate.ValueChanged += new System.EventHandler(this.dtpPrepDate_ValueChanged);
            // 
            // lblReceivedDate
            // 
            this.lblReceivedDate.AutoSize = true;
            this.lblReceivedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceivedDate.Location = new System.Drawing.Point(38, 32);
            this.lblReceivedDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceivedDate.Name = "lblReceivedDate";
            this.lblReceivedDate.Size = new System.Drawing.Size(138, 20);
            this.lblReceivedDate.TabIndex = 6;
            this.lblReceivedDate.Text = "Received Date:";
            this.lblReceivedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRecordsDate
            // 
            this.lblRecordsDate.AutoSize = true;
            this.lblRecordsDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsDate.Location = new System.Drawing.Point(48, 75);
            this.lblRecordsDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecordsDate.Name = "lblRecordsDate";
            this.lblRecordsDate.Size = new System.Drawing.Size(131, 20);
            this.lblRecordsDate.TabIndex = 4;
            this.lblRecordsDate.Text = "Records Date:";
            this.lblRecordsDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpRecordsDate
            // 
            this.dtpRecordsDate.CustomFormat = "MM/dd/yyyy HH:mm";
            this.dtpRecordsDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRecordsDate.Location = new System.Drawing.Point(190, 69);
            this.dtpRecordsDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpRecordsDate.Name = "dtpRecordsDate";
            this.dtpRecordsDate.Size = new System.Drawing.Size(265, 26);
            this.dtpRecordsDate.TabIndex = 6;
            this.dtpRecordsDate.Value = new System.DateTime(2012, 7, 12, 0, 0, 0, 0);
            this.dtpRecordsDate.ValueChanged += new System.EventHandler(this.dtpRecordsDate_ValueChanged);
            // 
            // pnlReceivedDate
            // 
            this.pnlReceivedDate.BackColor = System.Drawing.Color.Red;
            this.pnlReceivedDate.Location = new System.Drawing.Point(188, 23);
            this.pnlReceivedDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlReceivedDate.Name = "pnlReceivedDate";
            this.pnlReceivedDate.Size = new System.Drawing.Size(273, 37);
            this.pnlReceivedDate.TabIndex = 4;
            this.pnlReceivedDate.Visible = false;
            // 
            // pnlPrepDate
            // 
            this.pnlPrepDate.BackColor = System.Drawing.Color.Red;
            this.pnlPrepDate.Location = new System.Drawing.Point(188, 106);
            this.pnlPrepDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlPrepDate.Name = "pnlPrepDate";
            this.pnlPrepDate.Size = new System.Drawing.Size(273, 37);
            this.pnlPrepDate.TabIndex = 25;
            this.pnlPrepDate.Visible = false;
            // 
            // pnlRecordsDate
            // 
            this.pnlRecordsDate.BackColor = System.Drawing.Color.Red;
            this.pnlRecordsDate.Location = new System.Drawing.Point(188, 66);
            this.pnlRecordsDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlRecordsDate.Name = "pnlRecordsDate";
            this.pnlRecordsDate.Size = new System.Drawing.Size(273, 37);
            this.pnlRecordsDate.TabIndex = 24;
            this.pnlRecordsDate.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 446);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(250, 446);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 35);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblErrorText
            // 
            this.lblErrorText.AutoSize = true;
            this.lblErrorText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorText.ForeColor = System.Drawing.Color.Red;
            this.lblErrorText.Location = new System.Drawing.Point(18, 454);
            this.lblErrorText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblErrorText.Name = "lblErrorText";
            this.lblErrorText.Size = new System.Drawing.Size(0, 20);
            this.lblErrorText.TabIndex = 3;
            // 
            // BatchInfo
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(496, 497);
            this.Controls.Add(this.lblErrorText);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.grpSiteDept);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "BatchInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter Batch Information";
            this.Shown += new System.EventHandler(this.BatchInfo_Shown);
            this.grpSiteDept.ResumeLayout(false);
            this.grpSiteDept.PerformLayout();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSiteDept;
        private System.Windows.Forms.Label lblDepartment;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DateTimePicker dtpRecordsDate;
        private System.Windows.Forms.Label lblReceivedDate;
        private System.Windows.Forms.Label lblRecordsDate;
        private System.Windows.Forms.Label lblPrepDate;
        private System.Windows.Forms.DateTimePicker dtpPrepDate;
        private System.Windows.Forms.Label lblPrepOperator;
        private System.Windows.Forms.ComboBox lstPrepOperator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxNumber;
        private System.Windows.Forms.CheckBox chkRescan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOriginalBatchNo;
        private System.Windows.Forms.Label lblRescanReason;
        private System.Windows.Forms.ComboBox lstRescanReason;
        internal System.Windows.Forms.ComboBox lstDepartment;
        private System.Windows.Forms.Label lblPreppingIssue;
        private System.Windows.Forms.RadioButton rbNo;
        private System.Windows.Forms.RadioButton rbYes;
        private System.Windows.Forms.Label lblErrorText;
        private System.Windows.Forms.DateTimePicker dtpReceivedDate;
        private System.Windows.Forms.Panel pnlReceivedDate;
        private System.Windows.Forms.Panel pnlPrepDate;
        private System.Windows.Forms.Panel pnlRecordsDate;
    }
}