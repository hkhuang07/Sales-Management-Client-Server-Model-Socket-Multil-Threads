namespace ElectronicsStore.Presentation
{
    partial class frmEmployees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmployees));
            groupBox2 = new GroupBox();
            dataGridView = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            FullName = new DataGridViewTextBoxColumn();
            EmployeePhone = new DataGridViewTextBoxColumn();
            EmployeeAddress = new DataGridViewTextBoxColumn();
            UserName = new DataGridViewTextBoxColumn();
            Role = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label3 = new Label();
            txtEmployeeUsername = new TextBox();
            label5 = new Label();
            txtEmployeePassword = new TextBox();
            label6 = new Label();
            label2 = new Label();
            label4 = new Label();
            txtEmployeePhone = new TextBox();
            txtEmployeeAddress = new TextBox();
            txtEmployeeName = new TextBox();
            cboRoles = new ComboBox();
            toolStrip1 = new ToolStrip();
            btnBegin = new ToolStripButton();
            btnPrevious = new ToolStripButton();
            toolStripLabel3 = new ToolStripLabel();
            toolStripLabel1 = new ToolStripLabel();
            txtFind = new ToolStripTextBox();
            toolStripLabel2 = new ToolStripLabel();
            btnFind = new ToolStripButton();
            toolStripLabel4 = new ToolStripLabel();
            btnNext = new ToolStripButton();
            btnEnd = new ToolStripButton();
            toolStripLabel6 = new ToolStripLabel();
            lblMessage = new ToolStripLabel();
            btnImport = new ToolStripButton();
            btnExport = new ToolStripButton();
            btnClear = new ToolStripButton();
            groupBox1 = new GroupBox();
            label7 = new Label();
            btnClose = new Button();
            btnCancel = new Button();
            btnSave = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            helpProvider1 = new HelpProvider();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            toolStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBox2.Location = new Point(0, 213);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(784, 248);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Employees list:";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.LightBlue;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ID, FullName, EmployeePhone, EmployeeAddress, UserName, Role });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 21);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(778, 224);
            dataGridView.TabIndex = 0;
            // 
            // ID
            // 
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // FullName
            // 
            FullName.DataPropertyName = "FullName";
            FullName.HeaderText = "Employee Full Name";
            FullName.Name = "FullName";
            FullName.ReadOnly = true;
            // 
            // EmployeePhone
            // 
            EmployeePhone.DataPropertyName = "EmployeePhone";
            EmployeePhone.HeaderText = "Employee PhoneNumber";
            EmployeePhone.Name = "EmployeePhone";
            EmployeePhone.ReadOnly = true;
            // 
            // EmployeeAddress
            // 
            EmployeeAddress.DataPropertyName = "EmployeeAddress";
            EmployeeAddress.HeaderText = "Employee Address";
            EmployeeAddress.Name = "EmployeeAddress";
            EmployeeAddress.ReadOnly = true;
            // 
            // UserName
            // 
            UserName.DataPropertyName = "UserName";
            UserName.HeaderText = "Employee Username";
            UserName.Name = "UserName";
            UserName.ReadOnly = true;
            // 
            // Role
            // 
            Role.DataPropertyName = "Role";
            Role.HeaderText = "Employee Role";
            Role.Name = "Role";
            Role.ReadOnly = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.Location = new Point(32, 71);
            label1.Name = "label1";
            label1.Size = new Size(95, 17);
            label1.TabIndex = 0;
            label1.Text = "Full Name (*):";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(297, 71);
            label3.Name = "label3";
            label3.Size = new Size(93, 17);
            label3.TabIndex = 0;
            label3.Text = "Username (*):";
            // 
            // txtEmployeeUsername
            // 
            txtEmployeeUsername.Anchor = AnchorStyles.Top;
            txtEmployeeUsername.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtEmployeeUsername.Location = new Point(389, 68);
            txtEmployeeUsername.Name = "txtEmployeeUsername";
            txtEmployeeUsername.Size = new Size(143, 25);
            txtEmployeeUsername.TabIndex = 4;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label5.Location = new Point(297, 105);
            label5.Name = "label5";
            label5.Size = new Size(90, 17);
            label5.TabIndex = 0;
            label5.Text = "Password (*):";
            // 
            // txtEmployeePassword
            // 
            txtEmployeePassword.Anchor = AnchorStyles.Top;
            txtEmployeePassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtEmployeePassword.Location = new Point(389, 102);
            txtEmployeePassword.Name = "txtEmployeePassword";
            txtEmployeePassword.PasswordChar = '*';
            txtEmployeePassword.Size = new Size(143, 25);
            txtEmployeePassword.TabIndex = 5;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label6.Location = new Point(297, 135);
            label6.Name = "label6";
            label6.Size = new Size(65, 17);
            label6.TabIndex = 0;
            label6.Text = "Roles (*):";
            label6.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(32, 105);
            label2.Name = "label2";
            label2.Size = new Size(100, 17);
            label2.TabIndex = 0;
            label2.Text = "Phonenumber:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label4.Location = new Point(32, 138);
            label4.Name = "label4";
            label4.Size = new Size(61, 17);
            label4.TabIndex = 0;
            label4.Text = "Address:";
            // 
            // txtEmployeePhone
            // 
            txtEmployeePhone.Anchor = AnchorStyles.Top;
            txtEmployeePhone.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtEmployeePhone.Location = new Point(124, 102);
            txtEmployeePhone.Name = "txtEmployeePhone";
            txtEmployeePhone.Size = new Size(143, 25);
            txtEmployeePhone.TabIndex = 2;
            // 
            // txtEmployeeAddress
            // 
            txtEmployeeAddress.Anchor = AnchorStyles.Top;
            txtEmployeeAddress.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtEmployeeAddress.Location = new Point(124, 135);
            txtEmployeeAddress.Name = "txtEmployeeAddress";
            txtEmployeeAddress.Size = new Size(143, 25);
            txtEmployeeAddress.TabIndex = 3;
            // 
            // txtEmployeeName
            // 
            txtEmployeeName.Anchor = AnchorStyles.Top;
            txtEmployeeName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtEmployeeName.Location = new Point(124, 72);
            txtEmployeeName.Name = "txtEmployeeName";
            txtEmployeeName.Size = new Size(143, 25);
            txtEmployeeName.TabIndex = 1;
            // 
            // cboRoles
            // 
            cboRoles.Anchor = AnchorStyles.Top;
            cboRoles.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            cboRoles.FormattingEnabled = true;
            cboRoles.Items.AddRange(new object[] { " Admin", "User" });
            cboRoles.Location = new Point(389, 132);
            cboRoles.Name = "cboRoles";
            cboRoles.Size = new Size(143, 25);
            cboRoles.TabIndex = 6;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.MidnightBlue;
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnBegin, btnPrevious, toolStripLabel3, toolStripLabel1, txtFind, toolStripLabel2, btnFind, toolStripLabel4, btnNext, btnEnd, toolStripLabel6, lblMessage, btnImport, btnExport, btnClear });
            toolStrip1.Location = new Point(3, 166);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(778, 44);
            toolStrip1.TabIndex = 13;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnBegin
            // 
            btnBegin.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnBegin.Image = (Image)resources.GetObject("btnBegin.Image");
            btnBegin.ImageTransparentColor = Color.Magenta;
            btnBegin.Name = "btnBegin";
            btnBegin.Size = new Size(23, 41);
            btnBegin.Text = "toolStripButton1";
            // 
            // btnPrevious
            // 
            btnPrevious.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnPrevious.Image = (Image)resources.GetObject("btnPrevious.Image");
            btnPrevious.ImageTransparentColor = Color.Magenta;
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(23, 41);
            btnPrevious.Text = "toolStripButton2";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.AutoSize = false;
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(15, 30);
            toolStripLabel3.Text = "          ";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripLabel1.ForeColor = Color.WhiteSmoke;
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(47, 41);
            toolStripLabel1.Text = "Find:";
            // 
            // txtFind
            // 
            txtFind.Name = "txtFind";
            txtFind.Size = new Size(100, 44);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.AutoSize = false;
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(15, 30);
            toolStripLabel2.Text = "          ";
            // 
            // btnFind
            // 
            btnFind.AutoSize = false;
            btnFind.BackColor = Color.CornflowerBlue;
            btnFind.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFind.ForeColor = Color.White;
            btnFind.Image = (Image)resources.GetObject("btnFind.Image");
            btnFind.ImageAlign = ContentAlignment.MiddleRight;
            btnFind.ImageTransparentColor = Color.Magenta;
            btnFind.Margin = new Padding(7);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(60, 30);
            btnFind.Text = "Find";
            btnFind.TextAlign = ContentAlignment.MiddleLeft;
            btnFind.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // toolStripLabel4
            // 
            toolStripLabel4.AutoSize = false;
            toolStripLabel4.Name = "toolStripLabel4";
            toolStripLabel4.Size = new Size(15, 30);
            toolStripLabel4.Text = "          ";
            // 
            // btnNext
            // 
            btnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNext.Image = (Image)resources.GetObject("btnNext.Image");
            btnNext.ImageTransparentColor = Color.Magenta;
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(23, 41);
            btnNext.Text = "toolStripButton3";
            // 
            // btnEnd
            // 
            btnEnd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEnd.Image = (Image)resources.GetObject("btnEnd.Image");
            btnEnd.ImageTransparentColor = Color.Magenta;
            btnEnd.Name = "btnEnd";
            btnEnd.Size = new Size(23, 41);
            btnEnd.Text = "toolStripButton4";
            // 
            // toolStripLabel6
            // 
            toolStripLabel6.AutoSize = false;
            toolStripLabel6.Name = "toolStripLabel6";
            toolStripLabel6.Size = new Size(15, 30);
            toolStripLabel6.Text = "          ";
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblMessage.ForeColor = Color.WhiteSmoke;
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(0, 41);
            // 
            // btnImport
            // 
            btnImport.AutoSize = false;
            btnImport.BackColor = Color.DodgerBlue;
            btnImport.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImport.ForeColor = Color.White;
            btnImport.Image = (Image)resources.GetObject("btnImport.Image");
            btnImport.ImageAlign = ContentAlignment.MiddleRight;
            btnImport.ImageTransparentColor = Color.Magenta;
            btnImport.Margin = new Padding(7);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(80, 30);
            btnImport.Text = "Import";
            btnImport.TextAlign = ContentAlignment.MiddleLeft;
            btnImport.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnImport.Click += btnImport_Click;
            // 
            // btnExport
            // 
            btnExport.AutoSize = false;
            btnExport.BackColor = Color.CornflowerBlue;
            btnExport.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExport.ForeColor = Color.White;
            btnExport.Image = (Image)resources.GetObject("btnExport.Image");
            btnExport.ImageAlign = ContentAlignment.MiddleRight;
            btnExport.ImageTransparentColor = Color.Magenta;
            btnExport.Margin = new Padding(7);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(80, 30);
            btnExport.Text = "Export";
            btnExport.TextAlign = ContentAlignment.MiddleLeft;
            btnExport.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnExport.Click += btnExport_Click;
            // 
            // btnClear
            // 
            btnClear.AutoSize = false;
            btnClear.BackColor = Color.LightSlateGray;
            btnClear.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClear.ForeColor = Color.White;
            btnClear.Image = (Image)resources.GetObject("btnClear.Image");
            btnClear.ImageTransparentColor = Color.Magenta;
            btnClear.Margin = new Padding(7);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(80, 30);
            btnClear.Text = "Clear";
            btnClear.TextAlign = ContentAlignment.MiddleLeft;
            btnClear.TextImageRelation = TextImageRelation.TextBeforeImage;
            btnClear.Click += btnClear_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SkyBlue;
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnClose);
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(cboRoles);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(txtEmployeeName);
            groupBox1.Controls.Add(btnUpdate);
            groupBox1.Controls.Add(txtEmployeeAddress);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(txtEmployeePhone);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtEmployeePassword);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(txtEmployeeUsername);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(784, 213);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Employee Information :";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.LightCyan;
            label7.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.MidnightBlue;
            label7.Location = new Point(313, 21);
            label7.Name = "label7";
            label7.Size = new Size(158, 37);
            label7.TabIndex = 23;
            label7.Text = "Employees";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(678, 132);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 29);
            btnClose.TabIndex = 12;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top;
            btnCancel.BackColor = Color.DarkTurquoise;
            btnCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCancel.Location = new Point(678, 97);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top;
            btnSave.BackColor = Color.DeepSkyBlue;
            btnSave.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSave.Location = new Point(678, 62);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top;
            btnDelete.BackColor = Color.SteelBlue;
            btnDelete.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnDelete.ForeColor = Color.AliceBlue;
            btnDelete.Location = new Point(589, 132);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 29);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Top;
            btnUpdate.BackColor = Color.RoyalBlue;
            btnUpdate.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.AliceBlue;
            btnUpdate.Location = new Point(589, 97);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 29);
            btnUpdate.TabIndex = 8;
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
            btnAdd.Location = new Point(589, 62);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 29);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // frmEmployees
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmEmployees";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Employees";
            WindowState = FormWindowState.Maximized;
            Load += Employees_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox2;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn FullName;
        private DataGridViewTextBoxColumn EmployeePhone;
        private DataGridViewTextBoxColumn EmployeeAddress;
        private DataGridViewTextBoxColumn UserName;
        private DataGridViewTextBoxColumn Role;
        private Label label1;
        private Label label3;
        private TextBox txtEmployeeUsername;
        private Label label5;
        private TextBox txtEmployeePassword;
        private Label label6;
        private Label label2;
        private Label label4;
        private TextBox txtEmployeePhone;
        private TextBox txtEmployeeAddress;
        private TextBox txtEmployeeName;
        private ComboBox cboRoles;
        private ToolStrip toolStrip1;
        private ToolStripButton btnBegin;
        private ToolStripButton btnPrevious;
        private ToolStripLabel toolStripLabel3;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox txtFind;
        private ToolStripLabel toolStripLabel2;
        private ToolStripButton btnFind;
        private ToolStripLabel toolStripLabel4;
        private ToolStripButton btnNext;
        private ToolStripButton btnEnd;
        private ToolStripLabel toolStripLabel6;
        private ToolStripLabel lblMessage;
        private ToolStripButton btnImport;
        private ToolStripButton btnExport;
        private GroupBox groupBox1;
        private Button btnClose;
        private Button btnCancel;
        private Button btnSave;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private Label label7;
        private ToolStripButton btnClear;
        private HelpProvider helpProvider1;
    }
}