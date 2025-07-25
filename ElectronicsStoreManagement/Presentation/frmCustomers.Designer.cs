namespace ElectronicsStore.Presentation
{
    partial class frmCustomers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomers));
            groupBox1 = new GroupBox();
            label7 = new Label();
            btnClose = new Button();
            btnCancel = new Button();
            btnSave = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            toolStrip1 = new ToolStrip();
            btnBegin = new ToolStripButton();
            btnPrevious = new ToolStripButton();
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
            txtCustomerName = new TextBox();
            txtCustomerAddress = new TextBox();
            label2 = new Label();
            txtCustomerPhone = new TextBox();
            txtCustomerEmail = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dataGridView = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            CustomerName = new DataGridViewTextBoxColumn();
            CustomerEmail = new DataGridViewTextBoxColumn();
            CustomerPhone = new DataGridViewTextBoxColumn();
            CustomerAddress = new DataGridViewTextBoxColumn();
            helpProvider1 = new HelpProvider();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SkyBlue;
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnClose);
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(btnUpdate);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Controls.Add(txtCustomerName);
            groupBox1.Controls.Add(txtCustomerAddress);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtCustomerPhone);
            groupBox1.Controls.Add(txtCustomerEmail);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(684, 265);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Customer Information :";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.LightCyan;
            label7.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.MidnightBlue;
            label7.Location = new Point(264, 22);
            label7.Name = "label7";
            label7.Size = new Size(157, 37);
            label7.TabIndex = 23;
            label7.Text = "Customers";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(592, 141);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 29);
            btnClose.TabIndex = 10;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top;
            btnCancel.BackColor = Color.DarkTurquoise;
            btnCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCancel.Location = new Point(592, 106);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top;
            btnSave.BackColor = Color.DeepSkyBlue;
            btnSave.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSave.Location = new Point(592, 71);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 8;
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
            btnDelete.Location = new Point(503, 141);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 29);
            btnDelete.TabIndex = 7;
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
            btnUpdate.Location = new Point(503, 106);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 29);
            btnUpdate.TabIndex = 6;
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
            btnAdd.Location = new Point(503, 71);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 29);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.MidnightBlue;
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnBegin, btnPrevious, toolStripLabel1, txtFind, toolStripLabel2, btnFind, toolStripLabel4, btnNext, btnEnd, toolStripLabel6, lblMessage, btnImport, btnExport, btnClear });
            toolStrip1.Location = new Point(3, 218);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(678, 44);
            toolStrip1.TabIndex = 11;
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
            btnFind.BackColor = Color.RoyalBlue;
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
            //btnClear.Click += btnClear_Click;
            // 
            // txtCustomerName
            // 
            txtCustomerName.Anchor = AnchorStyles.Top;
            txtCustomerName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtCustomerName.Location = new Point(154, 71);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(303, 25);
            txtCustomerName.TabIndex = 1;
            // 
            // txtCustomerAddress
            // 
            txtCustomerAddress.Anchor = AnchorStyles.Top;
            txtCustomerAddress.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtCustomerAddress.Location = new Point(154, 178);
            txtCustomerAddress.Name = "txtCustomerAddress";
            txtCustomerAddress.Size = new Size(513, 25);
            txtCustomerAddress.TabIndex = 4;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(17, 181);
            label2.Name = "label2";
            label2.Size = new Size(61, 17);
            label2.TabIndex = 0;
            label2.Text = "Address:";
            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Anchor = AnchorStyles.Top;
            txtCustomerPhone.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtCustomerPhone.Location = new Point(154, 145);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.Size = new Size(303, 25);
            txtCustomerPhone.TabIndex = 3;
            // 
            // txtCustomerEmail
            // 
            txtCustomerEmail.Anchor = AnchorStyles.Top;
            txtCustomerEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtCustomerEmail.Location = new Point(154, 108);
            txtCustomerEmail.Name = "txtCustomerEmail";
            txtCustomerEmail.Size = new Size(303, 25);
            txtCustomerEmail.TabIndex = 2;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label4.Location = new Point(17, 148);
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
            label3.Location = new Point(17, 111);
            label3.Name = "label3";
            label3.Size = new Size(46, 17);
            label3.TabIndex = 0;
            label3.Text = "Email:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.Location = new Point(17, 74);
            label1.Name = "label1";
            label1.Size = new Size(131, 17);
            label1.TabIndex = 0;
            label1.Text = "Customer Name (*):";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(0, 265);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(684, 196);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Customers list:";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.LightBlue;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ID, CustomerName, CustomerEmail, CustomerPhone, CustomerAddress });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 21);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(678, 172);
            dataGridView.TabIndex = 0;
            // 
            // ID
            // 
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // CustomerName
            // 
            CustomerName.DataPropertyName = "CustomerName";
            CustomerName.HeaderText = "Customer Name";
            CustomerName.Name = "CustomerName";
            CustomerName.ReadOnly = true;
            // 
            // CustomerEmail
            // 
            CustomerEmail.DataPropertyName = "CustomerEmail";
            CustomerEmail.HeaderText = "Customer Email";
            CustomerEmail.Name = "CustomerEmail";
            CustomerEmail.ReadOnly = true;
            // 
            // CustomerPhone
            // 
            CustomerPhone.DataPropertyName = "CustomerPhone";
            CustomerPhone.HeaderText = "Customer Phone";
            CustomerPhone.Name = "CustomerPhone";
            CustomerPhone.ReadOnly = true;
            // 
            // CustomerAddress
            // 
            CustomerAddress.DataPropertyName = "CustomerAddress";
            CustomerAddress.HeaderText = "Customer Address";
            CustomerAddress.Name = "CustomerAddress";
            CustomerAddress.ReadOnly = true;
            // 
            // frmCustomers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmCustomers";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Customers";
            WindowState = FormWindowState.Maximized;
            Load += Customers_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox txtCustomerName;
        private TextBox txtCustomerAddress;
        private Label label2;
        private TextBox txtCustomerEmail;
        private Label label3;
        private Label label1;
        private GroupBox groupBox2;
        private DataGridView dataGridView;
        private TextBox txtCustomerPhone;
        private Label label4;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn CustomerName;
        private DataGridViewTextBoxColumn CustomerEmail;
        private DataGridViewTextBoxColumn CustomerPhone;
        private DataGridViewTextBoxColumn CustomerAddress;
        private ToolStrip toolStrip1;
        private ToolStripButton btnBegin;
        private ToolStripButton btnPrevious;
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
        private Button btnClose;
        private Button btnCancel;
        private Button btnSave;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private Label label7;
        private ToolStripButton btnClear;
        private HelpProvider helpProvider1;
        //private Microsoft.Reporting.WinForms.ReportToolBar.ToolStripButtonOverride toolStripButtonOverride1;
    }
}