namespace ElectronicsStore.Presentation
{
    partial class frmCategories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCategories));
            groupBox1 = new GroupBox();
            label7 = new Label();
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
            btnClose = new Button();
            btnCancel = new Button();
            btnSave = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            txtCategoryName = new TextBox();
            label1 = new Label();
            groupBox3 = new GroupBox();
            dataGridView = new DataGridView();
            ID = new DataGridViewTextBoxColumn();
            CategoryName = new DataGridViewTextBoxColumn();
            helpProvider1 = new HelpProvider();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SkyBlue;
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Controls.Add(btnClose);
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(btnSave);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(btnEdit);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(txtCategoryName);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(684, 181);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Category Information:";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.LightCyan;
            label7.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.MidnightBlue;
            label7.Location = new Point(263, 21);
            label7.Name = "label7";
            label7.Size = new Size(158, 37);
            label7.TabIndex = 23;
            label7.Text = "Categories";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = Color.MidnightBlue;
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnBegin, btnPrevious, toolStripLabel1, txtFind, toolStripLabel2, btnFind, toolStripLabel4, btnNext, btnEnd, toolStripLabel6, lblMessage, btnImport, btnExport, btnClear });
            toolStrip1.Location = new Point(3, 134);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(678, 44);
            toolStrip1.TabIndex = 8;
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
            btnClose.Location = new Point(527, 100);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 29);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top;
            btnCancel.BackColor = Color.DarkTurquoise;
            btnCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCancel.Location = new Point(438, 100);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 29);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top;
            btnSave.BackColor = Color.DeepSkyBlue;
            btnSave.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnSave.Location = new Point(349, 100);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 29);
            btnSave.TabIndex = 5;
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
            btnDelete.Location = new Point(260, 100);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 29);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Top;
            btnEdit.BackColor = Color.RoyalBlue;
            btnEdit.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnEdit.ForeColor = Color.AliceBlue;
            btnEdit.Location = new Point(171, 100);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 29);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Update";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top;
            btnAdd.BackColor = Color.Navy;
            btnAdd.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnAdd.ForeColor = Color.AliceBlue;
            btnAdd.Location = new Point(82, 100);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 29);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtCategoryName
            // 
            txtCategoryName.Anchor = AnchorStyles.Top;
            txtCategoryName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtCategoryName.Location = new Point(167, 67);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(435, 25);
            txtCategoryName.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.Location = new Point(37, 70);
            label1.Name = "label1";
            label1.Size = new Size(126, 17);
            label1.TabIndex = 0;
            label1.Text = "Category name (*):";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dataGridView);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(0, 181);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(684, 280);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Categories List";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.LightBlue;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ID, CategoryName });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 21);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(678, 256);
            dataGridView.TabIndex = 0;
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
            CategoryName.HeaderText = "Category Name";
            CategoryName.Name = "CategoryName";
            CategoryName.ReadOnly = true;
            // 
            // frmCategories
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmCategories";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Categories";
            WindowState = FormWindowState.Maximized;
            Load += Categories_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnClose;
        private Button btnCancel;
        private Button btnSave;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnAdd;
        private TextBox txtCategoryName;
        private Label label1;
        private GroupBox groupBox3;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn CategoryName;
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
        private Label label7;
        private ToolStripButton btnClear;
        private HelpProvider helpProvider1;
    }
}