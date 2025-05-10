namespace WindowsFormsApplication12
{
    partial class ManageCustomerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.rdoSearchBySerialId = new System.Windows.Forms.RadioButton();
            this.rdoSearchByUsername = new System.Windows.Forms.RadioButton();
            this.txtSearchInput = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDisplaySerialId = new System.Windows.Forms.TextBox();
            this.txtDisplayUsername = new System.Windows.Forms.TextBox();
            this.txtDisplayNationalId = new System.Windows.Forms.TextBox();
            this.txtDisplayAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbCustomerType = new System.Windows.Forms.ComboBox();
            this.btnUpdateType = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search By:";
            // 
            // rdoSearchBySerialId
            // 
            this.rdoSearchBySerialId.AutoSize = true;
            this.rdoSearchBySerialId.Location = new System.Drawing.Point(314, 151);
            this.rdoSearchBySerialId.Name = "rdoSearchBySerialId";
            this.rdoSearchBySerialId.Size = new System.Drawing.Size(79, 20);
            this.rdoSearchBySerialId.TabIndex = 1;
            this.rdoSearchBySerialId.TabStop = true;
            this.rdoSearchBySerialId.Text = "Serial ID";
            this.rdoSearchBySerialId.UseVisualStyleBackColor = true;
            // 
            // rdoSearchByUsername
            // 
            this.rdoSearchByUsername.AutoSize = true;
            this.rdoSearchByUsername.Location = new System.Drawing.Point(447, 151);
            this.rdoSearchByUsername.Name = "rdoSearchByUsername";
            this.rdoSearchByUsername.Size = new System.Drawing.Size(91, 20);
            this.rdoSearchByUsername.TabIndex = 2;
            this.rdoSearchByUsername.TabStop = true;
            this.rdoSearchByUsername.Text = "Username";
            this.rdoSearchByUsername.UseVisualStyleBackColor = true;
            // 
            // txtSearchInput
            // 
            this.txtSearchInput.Location = new System.Drawing.Point(384, 105);
            this.txtSearchInput.Name = "txtSearchInput";
            this.txtSearchInput.Size = new System.Drawing.Size(154, 22);
            this.txtSearchInput.TabIndex = 3;
            this.txtSearchInput.Enter += new System.EventHandler(this.txtSearchInput_Enter);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(278, 449);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(106, 30);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Serial ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(337, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(337, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "National ID:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 332);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Address:";
            // 
            // txtDisplaySerialId
            // 
            this.txtDisplaySerialId.Location = new System.Drawing.Point(428, 202);
            this.txtDisplaySerialId.Name = "txtDisplaySerialId";
            this.txtDisplaySerialId.ReadOnly = true;
            this.txtDisplaySerialId.Size = new System.Drawing.Size(100, 22);
            this.txtDisplaySerialId.TabIndex = 9;
            // 
            // txtDisplayUsername
            // 
            this.txtDisplayUsername.Location = new System.Drawing.Point(428, 242);
            this.txtDisplayUsername.Name = "txtDisplayUsername";
            this.txtDisplayUsername.ReadOnly = true;
            this.txtDisplayUsername.Size = new System.Drawing.Size(100, 22);
            this.txtDisplayUsername.TabIndex = 10;
            // 
            // txtDisplayNationalId
            // 
            this.txtDisplayNationalId.Location = new System.Drawing.Point(428, 289);
            this.txtDisplayNationalId.Name = "txtDisplayNationalId";
            this.txtDisplayNationalId.ReadOnly = true;
            this.txtDisplayNationalId.Size = new System.Drawing.Size(100, 22);
            this.txtDisplayNationalId.TabIndex = 11;
            // 
            // txtDisplayAddress
            // 
            this.txtDisplayAddress.Location = new System.Drawing.Point(428, 332);
            this.txtDisplayAddress.Name = "txtDisplayAddress";
            this.txtDisplayAddress.ReadOnly = true;
            this.txtDisplayAddress.Size = new System.Drawing.Size(100, 22);
            this.txtDisplayAddress.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(270, 387);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Customer Type:";
            // 
            // cmbCustomerType
            // 
            this.cmbCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerType.FormattingEnabled = true;
            this.cmbCustomerType.Location = new System.Drawing.Point(378, 379);
            this.cmbCustomerType.Name = "cmbCustomerType";
            this.cmbCustomerType.Size = new System.Drawing.Size(150, 24);
            this.cmbCustomerType.TabIndex = 14;
            // 
            // btnUpdateType
            // 
            this.btnUpdateType.Enabled = false;
            this.btnUpdateType.Location = new System.Drawing.Point(470, 449);
            this.btnUpdateType.Name = "btnUpdateType";
            this.btnUpdateType.Size = new System.Drawing.Size(106, 30);
            this.btnUpdateType.TabIndex = 15;
            this.btnUpdateType.Text = "Update Type";
            this.btnUpdateType.UseVisualStyleBackColor = true;
            this.btnUpdateType.Click += new System.EventHandler(this.btnUpdateType_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(399, 421);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "label7";
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlContent.Controls.Add(this.txtDisplayUsername);
            this.pnlContent.Controls.Add(this.txtDisplayNationalId);
            this.pnlContent.Controls.Add(this.lblStatus);
            this.pnlContent.Controls.Add(this.txtDisplayAddress);
            this.pnlContent.Controls.Add(this.btnSearch);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.txtSearchInput);
            this.pnlContent.Controls.Add(this.label5);
            this.pnlContent.Controls.Add(this.label6);
            this.pnlContent.Controls.Add(this.btnUpdateType);
            this.pnlContent.Controls.Add(this.label2);
            this.pnlContent.Controls.Add(this.label4);
            this.pnlContent.Controls.Add(this.rdoSearchByUsername);
            this.pnlContent.Controls.Add(this.rdoSearchBySerialId);
            this.pnlContent.Controls.Add(this.label3);
            this.pnlContent.Controls.Add(this.txtDisplaySerialId);
            this.pnlContent.Controls.Add(this.cmbCustomerType);
            this.pnlContent.Location = new System.Drawing.Point(3, 3);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(815, 604);
            this.pnlContent.TabIndex = 17;
            // 
            // ManageCustomerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Name = "ManageCustomerControl";
            this.Size = new System.Drawing.Size(868, 607);
            this.Load += new System.EventHandler(this.ManageCustomerControl_Load);
            this.Resize += new System.EventHandler(this.ManageCustomerControl_Resize);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoSearchBySerialId;
        private System.Windows.Forms.RadioButton rdoSearchByUsername;
        private System.Windows.Forms.TextBox txtSearchInput;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDisplaySerialId;
        private System.Windows.Forms.TextBox txtDisplayUsername;
        private System.Windows.Forms.TextBox txtDisplayNationalId;
        private System.Windows.Forms.TextBox txtDisplayAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbCustomerType;
        private System.Windows.Forms.Button btnUpdateType;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlContent;
    }
}
