namespace ElectronicsStore.Presentation
{
    partial class frmManufacturers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManufacturers));
            groupBox2 = new GroupBox();
            dataGridView = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            ManufacturerName = new DataGridViewTextBoxColumn();
            ManufacturerAddress = new DataGridViewTextBoxColumn();
            ManufacturerPhone = new DataGridViewTextBoxColumn();
            ManufacturerEmail = new DataGridViewTextBoxColumn();
            helpProvider1 = new HelpProvider();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnAdd = new Button();
            txtManufacturerName = new TextBox();
            btnEdit = new Button();
            txtManufacturerPhone = new TextBox();
            btnDelete = new Button();
            txtManufacturerAddress = new TextBox();
            btnSave = new Button();
            txtManufacturerEmail = new TextBox();
            btnCancel = new Button();
            miniToolStrip = new ToolStrip();
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
            btnClose = new Button();
            label7 = new Label();
            groupBox1 = new GroupBox();
            toolStrip1 = new ToolStrip();
            toolStrip2 = new ToolStrip();
            toolStripButton1 = new ToolStripButton();
            toolStripButton2 = new ToolStripButton();
            toolStripLabel3 = new ToolStripLabel();
            toolStripTextBox1 = new ToolStripTextBox();
            toolStripLabel5 = new ToolStripLabel();
            toolStripButton3 = new ToolStripButton();
            toolStripLabel7 = new ToolStripLabel();
            toolStripButton4 = new ToolStripButton();
            toolStripButton5 = new ToolStripButton();
            toolStripLabel8 = new ToolStripLabel();
            toolStripLabel9 = new ToolStripLabel();
            toolStripButton6 = new ToolStripButton();
            toolStripButton7 = new ToolStripButton();
            toolStripButton8 = new ToolStripButton();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            toolStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(0, 241);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(684, 220);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Manufacturer List";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.LightBlue;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ID, ManufacturerName, ManufacturerAddress, ManufacturerPhone, ManufacturerEmail });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 21);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(678, 196);
            dataGridView.TabIndex = 1;
            // 
            // ID
            // 
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // ManufacturerName
            // 
            ManufacturerName.DataPropertyName = "ManufacturerName";
            ManufacturerName.HeaderText = "Manufacturer Name";
            ManufacturerName.Name = "ManufacturerName";
            ManufacturerName.ReadOnly = true;
            // 
            // ManufacturerAddress
            // 
            ManufacturerAddress.DataPropertyName = "ManufacturerAddress";
            ManufacturerAddress.HeaderText = "Manufacturer Address";
            ManufacturerAddress.Name = "ManufacturerAddress";
            ManufacturerAddress.ReadOnly = true;
            // 
            // ManufacturerPhone
            // 
            ManufacturerPhone.DataPropertyName = "ManufacturerPhone";
            ManufacturerPhone.HeaderText = "Manufacturer Phone";
            ManufacturerPhone.Name = "ManufacturerPhone";
            ManufacturerPhone.ReadOnly = true;
            // 
            // ManufacturerEmail
            // 
            ManufacturerEmail.DataPropertyName = "ManufacturerEmail";
            ManufacturerEmail.HeaderText = "Manufacturer Email";
            ManufacturerEmail.Name = "ManufacturerEmail";
            ManufacturerEmail.ReadOnly = true;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.Location = new Point(27, 74);
            label1.Name = "label1";
            label1.Size = new Size(153, 17);
            label1.TabIndex = 0;
            label1.Text = "Manufacturer name (*):";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(27, 104);
            label2.Name = "label2";
            label2.Size = new Size(138, 17);
            label2.TabIndex = 0;
            label2.Text = "Manufacturer phone:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(27, 134);
            label3.Name = "label3";
            label3.Size = new Size(146, 17);
            label3.TabIndex = 0;
            label3.Text = "Manufacturer address:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label4.Location = new Point(347, 104);
            label4.Name = "label4";
            label4.Size = new Size(133, 17);
            label4.TabIndex = 0;
            label4.Text = "Manufacturer email:";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top;
            btnAdd.BackColor = Color.Navy;
            btnAdd.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnAdd.ForeColor = Color.AliceBlue;
            btnAdd.Location = new Point(77, 160);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 29);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtManufacturerName
            // 
            txtManufacturerName.Anchor = AnchorStyles.Top;
            txtManufacturerName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtManufacturerName.Location = new Point(190, 71);
            txtManufacturerName.Name = "txtManufacturerName";
            txtManufacturerName.Size = new Size(145, 25);
            txtManufacturerName.TabIndex = 1;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top;
            btnEdit.BackColor = Color.RoyalBlue;
            btnEdit.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnEdit.ForeColor = Color.AliceBlue;
            btnEdit.Location = new Point(166, 160);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 29);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "Update";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // txtManufacturerPhone
            // 
            txtManufacturerPhone.Anchor = AnchorStyles.Top;
            txtManufacturerPhone.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtManufacturerPhone.Location = new Point(190, 101);
            txtManufacturerPhone.Name = "txtManufacturerPhone";
            txtManufacturerPhone.Size = new Size(145, 25);
            txtManufacturerPhone.TabIndex = 2;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top;
            btnDelete.BackColor = Color.SteelBlue;
            btnDelete.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnDelete.ForeColor = Color.AliceBlue;
            btnDelete.Location = new Point(255, 160);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 29);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // txtManufacturerAddress
            // 
            txtManufacturerAddress.Anchor = AnchorStyles.Top;
            txtManufacturerAddress.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtManufacturerAddress.Location = new Point(190, 131);
            txtManufacturerAddress.Name = "txtManufacturerAddress";
            txtManufacturerAddress.Size = new Size(468, 25);
            txtManufacturerAddress.TabIndex = 4;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top;
            btnSave.BackColor = Color.DeepSkyBlue;
            btnSave.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSave.Location = new Point(344, 160);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtManufacturerEmail
            // 
            txtManufacturerEmail.Anchor = AnchorStyles.Top;
            txtManufacturerEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtManufacturerEmail.Location = new Point(486, 101);
            txtManufacturerEmail.Name = "txtManufacturerEmail";
            txtManufacturerEmail.Size = new Size(172, 25);
            txtManufacturerEmail.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top;
            btnCancel.BackColor = Color.DarkTurquoise;
            btnCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCancel.Location = new Point(433, 160);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // miniToolStrip
            // 
            miniToolStrip.AccessibleName = "New item selection";
            miniToolStrip.AccessibleRole = AccessibleRole.ButtonDropDown;
            miniToolStrip.AutoSize = false;
            miniToolStrip.BackColor = Color.MidnightBlue;
            miniToolStrip.CanOverflow = false;
            miniToolStrip.Dock = DockStyle.None;
            miniToolStrip.GripStyle = ToolStripGripStyle.Hidden;
            miniToolStrip.Location = new Point(0, 0);
            miniToolStrip.Name = "miniToolStrip";
            miniToolStrip.Size = new Size(678, 44);
            miniToolStrip.TabIndex = 0;
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
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(522, 160);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 29);
            btnClose.TabIndex = 10;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.LightCyan;
            label7.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.MidnightBlue;
            label7.Location = new Point(234, 19);
            label7.Name = "label7";
            label7.Size = new Size(217, 37);
            label7.TabIndex = 23;
            label7.Text = "Manufacturers";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SkyBlue;
            groupBox1.Controls.Add(toolStrip2);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnClose);
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(txtManufacturerEmail);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(txtManufacturerAddress);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(txtManufacturerPhone);
            groupBox1.Controls.Add(btnEdit);
            groupBox1.Controls.Add(txtManufacturerName);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(684, 241);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Manufacturer Information:";
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.MidnightBlue;
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnBegin, btnPrevious, toolStripLabel1, txtFind, toolStripLabel2, btnFind, toolStripLabel4, btnNext, btnEnd, toolStripLabel6, lblMessage, btnImport, btnExport, btnClear });
            toolStrip1.Location = new Point(3, 194);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(678, 44);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStrip2
            // 
            toolStrip2.BackColor = Color.MidnightBlue;
            toolStrip2.Dock = DockStyle.Bottom;
            toolStrip2.Items.AddRange(new ToolStripItem[] { toolStripButton1, toolStripButton2, toolStripLabel3, toolStripTextBox1, toolStripLabel5, toolStripButton3, toolStripLabel7, toolStripButton4, toolStripButton5, toolStripLabel8, toolStripLabel9, toolStripButton6, toolStripButton7, toolStripButton8 });
            toolStrip2.Location = new Point(3, 194);
            toolStrip2.Name = "toolStrip2";
            toolStrip2.Size = new Size(678, 44);
            toolStrip2.TabIndex = 24;
            toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 41);
            toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(23, 41);
            toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripLabel3.ForeColor = Color.WhiteSmoke;
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(47, 41);
            toolStripLabel3.Text = "Find:";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(100, 44);
            // 
            // toolStripLabel5
            // 
            toolStripLabel5.AutoSize = false;
            toolStripLabel5.Name = "toolStripLabel5";
            toolStripLabel5.Size = new Size(15, 30);
            toolStripLabel5.Text = "          ";
            // 
            // toolStripButton3
            // 
            toolStripButton3.AutoSize = false;
            toolStripButton3.BackColor = Color.CornflowerBlue;
            toolStripButton3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripButton3.ForeColor = Color.White;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageAlign = ContentAlignment.MiddleRight;
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Margin = new Padding(7);
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(60, 30);
            toolStripButton3.Text = "Find";
            toolStripButton3.TextAlign = ContentAlignment.MiddleLeft;
            toolStripButton3.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // toolStripLabel7
            // 
            toolStripLabel7.AutoSize = false;
            toolStripLabel7.Name = "toolStripLabel7";
            toolStripLabel7.Size = new Size(15, 30);
            toolStripLabel7.Text = "          ";
            // 
            // toolStripButton4
            // 
            toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton4.Image = (Image)resources.GetObject("toolStripButton4.Image");
            toolStripButton4.ImageTransparentColor = Color.Magenta;
            toolStripButton4.Name = "toolStripButton4";
            toolStripButton4.Size = new Size(23, 41);
            toolStripButton4.Text = "toolStripButton3";
            // 
            // toolStripButton5
            // 
            toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButton5.Image = (Image)resources.GetObject("toolStripButton5.Image");
            toolStripButton5.ImageTransparentColor = Color.Magenta;
            toolStripButton5.Name = "toolStripButton5";
            toolStripButton5.Size = new Size(23, 41);
            toolStripButton5.Text = "toolStripButton4";
            // 
            // toolStripLabel8
            // 
            toolStripLabel8.AutoSize = false;
            toolStripLabel8.Name = "toolStripLabel8";
            toolStripLabel8.Size = new Size(15, 30);
            toolStripLabel8.Text = "          ";
            // 
            // toolStripLabel9
            // 
            toolStripLabel9.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic, GraphicsUnit.Point, 0);
            toolStripLabel9.ForeColor = Color.WhiteSmoke;
            toolStripLabel9.Name = "toolStripLabel9";
            toolStripLabel9.Size = new Size(0, 41);
            // 
            // toolStripButton6
            // 
            toolStripButton6.AutoSize = false;
            toolStripButton6.BackColor = Color.DodgerBlue;
            toolStripButton6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripButton6.ForeColor = Color.White;
            toolStripButton6.Image = (Image)resources.GetObject("toolStripButton6.Image");
            toolStripButton6.ImageAlign = ContentAlignment.MiddleRight;
            toolStripButton6.ImageTransparentColor = Color.Magenta;
            toolStripButton6.Margin = new Padding(7);
            toolStripButton6.Name = "toolStripButton6";
            toolStripButton6.Size = new Size(80, 30);
            toolStripButton6.Text = "Import";
            toolStripButton6.TextAlign = ContentAlignment.MiddleLeft;
            toolStripButton6.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // toolStripButton7
            // 
            toolStripButton7.AutoSize = false;
            toolStripButton7.BackColor = Color.CornflowerBlue;
            toolStripButton7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripButton7.ForeColor = Color.White;
            toolStripButton7.Image = (Image)resources.GetObject("toolStripButton7.Image");
            toolStripButton7.ImageAlign = ContentAlignment.MiddleRight;
            toolStripButton7.ImageTransparentColor = Color.Magenta;
            toolStripButton7.Margin = new Padding(7);
            toolStripButton7.Name = "toolStripButton7";
            toolStripButton7.Size = new Size(80, 30);
            toolStripButton7.Text = "Export";
            toolStripButton7.TextAlign = ContentAlignment.MiddleLeft;
            toolStripButton7.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // toolStripButton8
            // 
            toolStripButton8.AutoSize = false;
            toolStripButton8.BackColor = Color.LightSlateGray;
            toolStripButton8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripButton8.ForeColor = Color.White;
            toolStripButton8.Image = (Image)resources.GetObject("toolStripButton8.Image");
            toolStripButton8.ImageTransparentColor = Color.Magenta;
            toolStripButton8.Margin = new Padding(7);
            toolStripButton8.Name = "toolStripButton8";
            toolStripButton8.Size = new Size(80, 30);
            toolStripButton8.Text = "Clear";
            toolStripButton8.TextAlign = ContentAlignment.MiddleLeft;
            toolStripButton8.TextImageRelation = TextImageRelation.TextBeforeImage;
            // 
            // frmManufacturers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmManufacturers";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manufacturers";
            WindowState = FormWindowState.Maximized;
            Load += frmManufacturers_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private GroupBox groupBox2;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn ManufacturerName;
        private DataGridViewTextBoxColumn ManufacturerAddress;
        private DataGridViewTextBoxColumn ManufacturerPhone;
        private DataGridViewTextBoxColumn ManufacturerEmail;
        private HelpProvider helpProvider1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnAdd;
        private TextBox txtManufacturerName;
        private Button btnEdit;
        private TextBox txtManufacturerPhone;
        private Button btnDelete;
        private TextBox txtManufacturerAddress;
        private Button btnSave;
        private TextBox txtManufacturerEmail;
        private Button btnCancel;
        private ToolStrip miniToolStrip;
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
        private ToolStripButton btnClear;
        private Button btnClose;
        private Label label7;
        private GroupBox groupBox1;
        private ToolStrip toolStrip1;
        private ToolStrip toolStrip2;
        private ToolStripButton toolStripButton1;
        private ToolStripButton toolStripButton2;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripLabel toolStripLabel5;
        private ToolStripButton toolStripButton3;
        private ToolStripLabel toolStripLabel7;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton5;
        private ToolStripLabel toolStripLabel8;
        private ToolStripLabel toolStripLabel9;
        private ToolStripButton toolStripButton6;
        private ToolStripButton toolStripButton7;
        private ToolStripButton toolStripButton8;
    }
}