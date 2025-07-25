namespace ElectronicsStore.Presentation
{
    partial class frmConfirm
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
            groupBox1 = new GroupBox();
            chkPrintInvoice = new CheckBox();
            cboCustomer = new ComboBox();
            cboEmployee = new ComboBox();
            label7 = new Label();
            btnClose = new Button();
            btnConfirm = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtNote = new TextBox();
            txtCustomerAddress = new TextBox();
            label6 = new Label();
            label2 = new Label();
            txtCustomerPhone = new TextBox();
            txtCustomerEmail = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            label1 = new Label();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SkyBlue;
            groupBox1.Controls.Add(chkPrintInvoice);
            groupBox1.Controls.Add(cboCustomer);
            groupBox1.Controls.Add(cboEmployee);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnClose);
            groupBox1.Controls.Add(btnConfirm);
            groupBox1.Controls.Add(btnUpdate);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(txtNote);
            groupBox1.Controls.Add(txtCustomerAddress);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtCustomerPhone);
            groupBox1.Controls.Add(txtCustomerEmail);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(684, 361);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Customer Information :";
            // 
            // chkPrintInvoice
            // 
            chkPrintInvoice.AutoSize = true;
            chkPrintInvoice.Location = new Point(131, 182);
            chkPrintInvoice.Name = "chkPrintInvoice";
            chkPrintInvoice.Size = new Size(105, 21);
            chkPrintInvoice.TabIndex = 6;
            chkPrintInvoice.Text = "Print Invoice";
            chkPrintInvoice.UseVisualStyleBackColor = true;
            // 
            // cboCustomer
            // 
            cboCustomer.FormattingEnabled = true;
            cboCustomer.Location = new Point(131, 70);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(218, 25);
            cboCustomer.TabIndex = 1;
            cboCustomer.Text = "No Name";
            // 
            // cboEmployee
            // 
            cboEmployee.FormattingEnabled = true;
            cboEmployee.Location = new Point(465, 70);
            cboEmployee.Name = "cboEmployee";
            cboEmployee.Size = new Size(168, 25);
            cboEmployee.TabIndex = 2;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.LightCyan;
            label7.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.MidnightBlue;
            label7.Location = new Point(196, 21);
            label7.Name = "label7";
            label7.Size = new Size(293, 37);
            label7.TabIndex = 0;
            label7.Text = "Confirm Information";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(549, 319);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 30);
            btnClose.TabIndex = 11;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Top;
            btnConfirm.BackColor = Color.DeepSkyBlue;
            btnConfirm.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnConfirm.Location = new Point(383, 319);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(80, 30);
            btnConfirm.TabIndex = 10;
            btnConfirm.Text = "Confirm ";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Top;
            btnUpdate.BackColor = Color.RoyalBlue;
            btnUpdate.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.AliceBlue;
            btnUpdate.Location = new Point(217, 319);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 30);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top;
            btnAdd.BackColor = Color.Navy;
            btnAdd.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnAdd.ForeColor = Color.AliceBlue;
            btnAdd.Location = new Point(51, 319);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtNote
            // 
            txtNote.Anchor = AnchorStyles.Top;
            txtNote.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtNote.Location = new Point(129, 209);
            txtNote.Multiline = true;
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(504, 88);
            txtNote.TabIndex = 7;
            // 
            // txtCustomerAddress
            // 
            txtCustomerAddress.Anchor = AnchorStyles.Top;
            txtCustomerAddress.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtCustomerAddress.Location = new Point(129, 144);
            txtCustomerAddress.Name = "txtCustomerAddress";
            txtCustomerAddress.Size = new Size(504, 25);
            txtCustomerAddress.TabIndex = 5;
            txtCustomerAddress.Text = "No Address";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label6.Location = new Point(51, 212);
            label6.Name = "label6";
            label6.Size = new Size(42, 17);
            label6.TabIndex = 0;
            label6.Text = "Note:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(51, 147);
            label2.Name = "label2";
            label2.Size = new Size(61, 17);
            label2.TabIndex = 0;
            label2.Text = "Address:";
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Anchor = AnchorStyles.Top;
            txtCustomerPhone.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtCustomerPhone.Location = new Point(492, 107);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.Size = new Size(141, 25);
            txtCustomerPhone.TabIndex = 4;
            txtCustomerPhone.Text = "+8400000000";
            // 
            // txtCustomerEmail
            // 
            txtCustomerEmail.Anchor = AnchorStyles.Top;
            txtCustomerEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtCustomerEmail.Location = new Point(129, 107);
            txtCustomerEmail.Name = "txtCustomerEmail";
            txtCustomerEmail.Size = new Size(220, 25);
            txtCustomerEmail.TabIndex = 3;
            txtCustomerEmail.Text = "mail@gmail.com";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label4.Location = new Point(355, 110);
            label4.Name = "label4";
            label4.Size = new Size(106, 17);
            label4.TabIndex = 0;
            label4.Text = "Phone Number:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(51, 110);
            label3.Name = "label3";
            label3.Size = new Size(46, 17);
            label3.TabIndex = 0;
            label3.Text = "Email:";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label5.Location = new Point(355, 78);
            label5.Name = "label5";
            label5.Size = new Size(72, 17);
            label5.TabIndex = 0;
            label5.Text = "Employee:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.Location = new Point(51, 73);
            label1.Name = "label1";
            label1.Size = new Size(72, 17);
            label1.TabIndex = 0;
            label1.Text = "Customer:";
            // 
            // frmConfirm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 361);
            Controls.Add(groupBox1);
            Name = "frmConfirm";
            Text = "Add Customer";
            Load += frmConfirm_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label7;
        private Button btnClose;
        private Button btnConfirm;
        private Button btnUpdate;
        private Button btnAdd;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label1;
        public TextBox txtCustomerAddress;
        public TextBox txtCustomerPhone;
        public TextBox txtCustomerEmail;
        private Label label5;
        public ComboBox cboEmployee;
        public TextBox txtNote;
        private Label label6;
        public ComboBox cboCustomer;
        public CheckBox chkPrintInvoice;
    }
}