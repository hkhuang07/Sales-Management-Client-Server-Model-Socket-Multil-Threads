namespace ElectronicsStore.Presentation
{
    partial class frmProducts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProducts));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            label7 = new Label();
            btnClose = new Button();
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
            btnCancel = new Button();
            picImage = new PictureBox();
            btnSave = new Button();
            txtDescription = new TextBox();
            btnDelete = new Button();
            txtProductName = new TextBox();
            btnUpdate = new Button();
            numPrice = new NumericUpDown();
            btnAdd = new Button();
            numQuantity = new NumericUpDown();
            cboManufacturer = new ComboBox();
            cboCategory = new ComboBox();
            btnChangeImage = new Button();
            label6 = new Label();
            label4 = new Label();
            label5 = new Label();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dataGridView = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            CategoryName = new DataGridViewTextBoxColumn();
            ManufacturerName = new DataGridViewTextBoxColumn();
            ProductName = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            Description = new DataGridViewTextBoxColumn();
            Image = new DataGridViewImageColumn();
            helpProvider1 = new HelpProvider();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SkyBlue;
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(btnClose);
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(picImage);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(txtDescription);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(txtProductName);
            groupBox1.Controls.Add(btnUpdate);
            groupBox1.Controls.Add(numPrice);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(numQuantity);
            groupBox1.Controls.Add(cboManufacturer);
            groupBox1.Controls.Add(cboCategory);
            groupBox1.Controls.Add(btnChangeImage);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(784, 281);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Product Information :";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.LightCyan;
            label7.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.MidnightBlue;
            label7.Location = new Point(325, 19);
            label7.Name = "label7";
            label7.Size = new Size(134, 37);
            label7.TabIndex = 21;
            label7.Text = "Products";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(583, 195);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 29);
            btnClose.TabIndex = 13;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.MidnightBlue;
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnBegin, btnPrevious, toolStripLabel3, toolStripLabel1, txtFind, toolStripLabel2, btnFind, toolStripLabel4, btnNext, btnEnd, toolStripLabel6, lblMessage, btnImport, btnExport, btnClear });
            toolStrip1.Location = new Point(3, 234);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(778, 44);
            toolStrip1.TabIndex = 20;
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
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top;
            btnCancel.BackColor = Color.DarkTurquoise;
            btnCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCancel.Location = new Point(494, 195);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // picImage
            // 
            picImage.Anchor = AnchorStyles.Top;
            picImage.Location = new Point(572, 61);
            picImage.Name = "picImage";
            picImage.Size = new Size(102, 119);
            picImage.SizeMode = PictureBoxSizeMode.StretchImage;
            picImage.TabIndex = 11;
            picImage.TabStop = false;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top;
            btnSave.BackColor = Color.DeepSkyBlue;
            btnSave.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSave.Location = new Point(405, 195);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top;
            txtDescription.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtDescription.Location = new Point(138, 157);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(415, 25);
            txtDescription.TabIndex = 4;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top;
            btnDelete.BackColor = Color.SteelBlue;
            btnDelete.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnDelete.ForeColor = Color.AliceBlue;
            btnDelete.Location = new Point(316, 195);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 29);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // txtProductName
            // 
            txtProductName.Anchor = AnchorStyles.Top;
            txtProductName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtProductName.Location = new Point(138, 128);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(415, 25);
            txtProductName.TabIndex = 3;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Top;
            btnUpdate.BackColor = Color.RoyalBlue;
            btnUpdate.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.AliceBlue;
            btnUpdate.Location = new Point(227, 195);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 29);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // numPrice
            // 
            numPrice.Anchor = AnchorStyles.Top;
            numPrice.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            numPrice.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numPrice.Location = new Point(433, 97);
            numPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(120, 25);
            numPrice.TabIndex = 6;
            numPrice.ThousandsSeparator = true;
            numPrice.Value = new decimal(new int[] { 1000000, 0, 0, 0 });
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top;
            btnAdd.BackColor = Color.Navy;
            btnAdd.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnAdd.ForeColor = Color.AliceBlue;
            btnAdd.Location = new Point(138, 195);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 29);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // numQuantity
            // 
            numQuantity.Anchor = AnchorStyles.Top;
            numQuantity.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            numQuantity.Location = new Point(433, 68);
            numQuantity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(120, 25);
            numQuantity.TabIndex = 5;
            numQuantity.ThousandsSeparator = true;
            numQuantity.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // cboManufacturer
            // 
            cboManufacturer.Anchor = AnchorStyles.Top;
            cboManufacturer.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            cboManufacturer.FormattingEnabled = true;
            cboManufacturer.Location = new Point(138, 96);
            cboManufacturer.Name = "cboManufacturer";
            cboManufacturer.Size = new Size(177, 25);
            cboManufacturer.TabIndex = 2;
            // 
            // cboCategory
            // 
            cboCategory.Anchor = AnchorStyles.Top;
            cboCategory.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(138, 67);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(177, 25);
            cboCategory.TabIndex = 1;
            // 
            // btnChangeImage
            // 
            btnChangeImage.Anchor = AnchorStyles.Top;
            btnChangeImage.BackColor = Color.DarkBlue;
            btnChangeImage.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnChangeImage.ForeColor = Color.AliceBlue;
            btnChangeImage.Location = new Point(680, 61);
            btnChangeImage.Name = "btnChangeImage";
            btnChangeImage.Size = new Size(87, 46);
            btnChangeImage.TabIndex = 7;
            btnChangeImage.Text = "Change Picture...";
            btnChangeImage.UseVisualStyleBackColor = false;
            btnChangeImage.Click += btnChangeImage_Click;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label6.Location = new Point(18, 160);
            label6.Name = "label6";
            label6.Size = new Size(83, 17);
            label6.TabIndex = 0;
            label6.Text = "Description:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label4.Location = new Point(17, 131);
            label4.Name = "label4";
            label4.Size = new Size(116, 17);
            label4.TabIndex = 0;
            label4.Text = "Product Name(*):\r\n";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label5.Location = new Point(338, 99);
            label5.Name = "label5";
            label5.Size = new Size(92, 17);
            label5.TabIndex = 0;
            label5.Text = "Unit Price (*):";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(18, 99);
            label2.Name = "label2";
            label2.Size = new Size(115, 17);
            label2.TabIndex = 0;
            label2.Text = "Manufacturer (*):";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(338, 70);
            label3.Name = "label3";
            label3.Size = new Size(86, 17);
            label3.TabIndex = 0;
            label3.Text = "Quantity (*):";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.Location = new Point(18, 70);
            label1.Name = "label1";
            label1.Size = new Size(88, 17);
            label1.TabIndex = 0;
            label1.Text = "Category (*):";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(0, 281);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(784, 280);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Customer list:";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.LightBlue;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ID, CategoryName, ManufacturerName, ProductName, Price, Quantity, Description, Image });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 21);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(778, 256);
            dataGridView.TabIndex = 0;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            // 
            // ID
            // 
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // CategoryName
            // 
            CategoryName.DataPropertyName = "CategoryName";
            CategoryName.HeaderText = "Category";
            CategoryName.Name = "CategoryName";
            CategoryName.ReadOnly = true;
            // 
            // ManufacturerName
            // 
            ManufacturerName.DataPropertyName = "ManufacturerName";
            ManufacturerName.HeaderText = "Manufacturer";
            ManufacturerName.Name = "ManufacturerName";
            ManufacturerName.ReadOnly = true;
            // 
            // ProductName
            // 
            ProductName.DataPropertyName = "ProductName";
            ProductName.HeaderText = "Product Name";
            ProductName.Name = "ProductName";
            ProductName.ReadOnly = true;
            // 
            // Price
            // 
            Price.DataPropertyName = "Price";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            Price.DefaultCellStyle = dataGridViewCellStyle1;
            Price.HeaderText = "Unit Price";
            Price.Name = "Price";
            Price.ReadOnly = true;
            // 
            // Quantity
            // 
            Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            Quantity.DefaultCellStyle = dataGridViewCellStyle2;
            Quantity.HeaderText = "Quantity";
            Quantity.Name = "Quantity";
            Quantity.ReadOnly = true;
            // 
            // Description
            // 
            Description.DataPropertyName = "Description";
            Description.HeaderText = "Description";
            Description.Name = "Description";
            Description.ReadOnly = true;
            // 
            // Image
            // 
            Image.DataPropertyName = "Image";
            Image.HeaderText = "Image";
            Image.Name = "Image";
            Image.ReadOnly = true;
            // 
            // frmProducts
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmProducts";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Products";
            WindowState = FormWindowState.Maximized;
            Load += frmProducts_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private PictureBox picImage;
        private TextBox txtDescription;
        private TextBox txtProductName;
        private NumericUpDown numPrice;
        private NumericUpDown numQuantity;
        private ComboBox cboManufacturer;
        private ComboBox cboCategory;
        private Button btnChangeImage;
        private Label label6;
        private Label label4;
        private Label label5;
        private Label label2;
        private Label label3;
        private Label label1;
        private GroupBox groupBox2;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn CategoryName;
        private DataGridViewTextBoxColumn ManufacturerName;
        private DataGridViewTextBoxColumn ProductName;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewImageColumn Image;
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
        private Button btnClose;
        private Button btnCancel;
        private Button btnSave;
        private Button btnDelete;
        private Button btnUpdate;
        private Label label7;
        private ToolStripButton btnClear;
        private HelpProvider helpProvider1;
        private Button btnAdd;
    }
}