namespace ElectronicsStore.Presentation
{
    partial class frmOrderDetails
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            label7 = new Label();
            groupBox4 = new GroupBox();
            numQuantity = new NumericUpDown();
            numPrice = new NumericUpDown();
            btnDelete = new Button();
            btnConfirm = new Button();
            label5 = new Label();
            label4 = new Label();
            label1 = new Label();
            cboProduct = new ComboBox();
            txtNote = new TextBox();
            label6 = new Label();
            cboCustomer = new ComboBox();
            cboEmployee = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            groupBox3 = new GroupBox();
            btnSave = new Button();
            btnPrint = new Button();
            btnClose = new Button();
            groupBox2 = new GroupBox();
            dataGridView = new DataGridView();
            ProductID = new DataGridViewTextBoxColumn();
            ProductName = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Quantity = new DataGridViewTextBoxColumn();
            TotalPrice = new DataGridViewTextBoxColumn();
            helpProvider1 = new HelpProvider();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.SkyBlue;
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(txtNote);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(cboCustomer);
            groupBox1.Controls.Add(cboEmployee);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(784, 232);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Order Details Information:";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top;
            label7.AutoSize = true;
            label7.BackColor = Color.LightCyan;
            label7.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.MidnightBlue;
            label7.Location = new Point(295, 21);
            label7.Name = "label7";
            label7.Size = new Size(194, 37);
            label7.TabIndex = 22;
            label7.Text = "Order Details";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox4
            // 
            groupBox4.BackColor = Color.LightSteelBlue;
            groupBox4.Controls.Add(numQuantity);
            groupBox4.Controls.Add(numPrice);
            groupBox4.Controls.Add(btnDelete);
            groupBox4.Controls.Add(btnConfirm);
            groupBox4.Controls.Add(label5);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(cboProduct);
            groupBox4.Dock = DockStyle.Bottom;
            groupBox4.ForeColor = Color.Black;
            groupBox4.Location = new Point(3, 143);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(778, 86);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Order Detail Information:";
            // 
            // numQuantity
            // 
            numQuantity.Anchor = AnchorStyles.Top;
            numQuantity.Location = new Point(345, 47);
            numQuantity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(120, 25);
            numQuantity.TabIndex = 3;
            numQuantity.ThousandsSeparator = true;
            // 
            // numPrice
            // 
            numPrice.Anchor = AnchorStyles.Top;
            numPrice.Location = new Point(204, 46);
            numPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(120, 25);
            numPrice.TabIndex = 2;
            numPrice.ThousandsSeparator = true;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top;
            btnDelete.BackColor = Color.SteelBlue;
            btnDelete.ForeColor = Color.AliceBlue;
            btnDelete.Location = new Point(674, 39);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(86, 36);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Anchor = AnchorStyles.Top;
            btnConfirm.BackColor = Color.Navy;
            btnConfirm.ForeColor = Color.AliceBlue;
            btnConfirm.Location = new Point(573, 39);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(86, 36);
            btnConfirm.TabIndex = 4;
            btnConfirm.Text = "Confirm ";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Location = new Point(345, 28);
            label5.Name = "label5";
            label5.Size = new Size(62, 17);
            label5.TabIndex = 0;
            label5.Text = "Quantity";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Location = new Point(204, 28);
            label4.Name = "label4";
            label4.Size = new Size(92, 17);
            label4.TabIndex = 0;
            label4.Text = "Unit Price (*):";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(9, 28);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 0;
            label1.Text = "Product (*):";
            // 
            // cboProduct
            // 
            cboProduct.Anchor = AnchorStyles.Top;
            cboProduct.FormattingEnabled = true;
            cboProduct.Location = new Point(9, 46);
            cboProduct.Name = "cboProduct";
            cboProduct.Size = new Size(180, 25);
            cboProduct.TabIndex = 1;
            cboProduct.SelectionChangeCommitted += cboProduct_SelectionChangeCommitted;
            // 
            // txtNote
            // 
            txtNote.Anchor = AnchorStyles.Top;
            txtNote.Location = new Point(103, 103);
            txtNote.Name = "txtNote";
            txtNote.Size = new Size(669, 25);
            txtNote.TabIndex = 3;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Location = new Point(12, 74);
            label6.Name = "label6";
            label6.Size = new Size(65, 17);
            label6.TabIndex = 0;
            label6.Text = " Staff (*):";
            // 
            // cboCustomer
            // 
            cboCustomer.Anchor = AnchorStyles.Top;
            cboCustomer.FormattingEnabled = true;
            cboCustomer.Location = new Point(520, 71);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(252, 25);
            cboCustomer.TabIndex = 2;
            // 
            // cboEmployee
            // 
            cboEmployee.Anchor = AnchorStyles.Top;
            cboEmployee.FormattingEnabled = true;
            cboEmployee.Location = new Point(103, 71);
            cboEmployee.Name = "cboEmployee";
            cboEmployee.Size = new Size(281, 25);
            cboEmployee.TabIndex = 1;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Location = new Point(12, 103);
            label2.Name = "label2";
            label2.Size = new Size(85, 17);
            label2.TabIndex = 0;
            label2.Text = "Order Note :";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Location = new Point(422, 74);
            label3.Name = "label3";
            label3.Size = new Size(91, 17);
            label3.TabIndex = 0;
            label3.Text = "Customer (*):";
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.SkyBlue;
            groupBox3.Controls.Add(btnSave);
            groupBox3.Controls.Add(btnPrint);
            groupBox3.Controls.Add(btnClose);
            groupBox3.Dock = DockStyle.Bottom;
            groupBox3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBox3.Location = new Point(0, 504);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(784, 57);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top;
            btnSave.BackColor = Color.DeepSkyBlue;
            btnSave.Location = new Point(201, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(98, 37);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save Order...";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Top;
            btnPrint.AutoEllipsis = true;
            btnPrint.BackColor = Color.DodgerBlue;
            btnPrint.Location = new Point(352, 10);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(104, 37);
            btnPrint.TabIndex = 1;
            btnPrint.Text = "Print Order...";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Location = new Point(501, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(82, 37);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            groupBox2.Location = new Point(0, 232);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(784, 272);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.BackgroundColor = Color.LightBlue;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ProductID, ProductName, Price, Quantity, TotalPrice });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 21);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(778, 248);
            dataGridView.TabIndex = 0;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            dataGridView.SelectionChanged += dataGridView_SelectionChanged;
            // 
            // ProductID
            // 
            ProductID.DataPropertyName = "ProductID";
            ProductID.HeaderText = "ID";
            ProductID.Name = "ProductID";
            ProductID.ReadOnly = true;
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
            // TotalPrice
            // 
            TotalPrice.DataPropertyName = "TotalPrice";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.Blue;
            dataGridViewCellStyle3.Format = "N0";
            TotalPrice.DefaultCellStyle = dataGridViewCellStyle3;
            TotalPrice.HeaderText = "TotalPrice";
            TotalPrice.Name = "TotalPrice";
            TotalPrice.ReadOnly = true;
            // 
            // frmOrderDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(groupBox2);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmOrderDetails";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Order Details";
            Load += frmOrderDetails_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private GroupBox groupBox4;
        private NumericUpDown numQuantity;
        private NumericUpDown numPrice;
        private Button btnDelete;
        private Button btnConfirm;
        private Label label5;
        private Label label4;
        private Label label1;
        private ComboBox cboProduct;
        private TextBox txtNote;
        private Label label6;
        private ComboBox cboCustomer;
        private ComboBox cboEmployee;
        private Label label2;
        private Label label3;
        private GroupBox groupBox3;
        private Button btnSave;
        private Button btnPrint;
        private Button btnClose;
        private GroupBox groupBox2;
        private DataGridView dataGridView;
        private DataGridViewTextBoxColumn ProductID;
        private DataGridViewTextBoxColumn ProductName;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Quantity;
        private DataGridViewTextBoxColumn TotalPrice;
        private Label label7;
        private HelpProvider helpProvider1;
    }
}