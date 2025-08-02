namespace ElectronicsStore.Presentation
{
    partial class frmSale
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
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            panel1 = new Panel();
            tabControl1 = new TabControl();
            tabOrderDetails = new TabPage();
            dgvOrderDetails = new DataGridView();
            ProductID = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            listView1 = new ListView();
            ID = new ColumnHeader();
            ProductName = new ColumnHeader();
            Quantity = new ColumnHeader();
            Price = new ColumnHeader();
            panel4 = new Panel();
            btnOrder = new Button();
            txtTotalDetails = new TextBox();
            label2 = new Label();
            tabPage2 = new TabPage();
            panel6 = new Panel();
            btnPrint = new Button();
            dgvOrder = new DataGridView();
            OrderID = new DataGridViewTextBoxColumn();
            EmployeeName = new DataGridViewTextBoxColumn();
            CustomerName = new DataGridViewTextBoxColumn();
            Date = new DataGridViewTextBoxColumn();
            TotalPrice = new DataGridViewTextBoxColumn();
            ViewDetails = new DataGridViewLinkColumn();
            panel5 = new Panel();
            btnPay = new Button();
            btnFilter = new Button();
            dtpEnd = new DateTimePicker();
            dtpStart = new DateTimePicker();
            txtRevenue = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            txtFind = new TextBox();
            label6 = new Label();
            btnFind = new Button();
            btnCancel = new Button();
            btnClose = new Button();
            bntDelete = new Button();
            btnAdd = new Button();
            panel3 = new Panel();
            statusStrip1 = new StatusStrip();
            sttEmployee = new ToolStripStatusLabel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            helpProvider1 = new HelpProvider();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).BeginInit();
            panel4.SuspendLayout();
            tabPage2.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOrder).BeginInit();
            panel5.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(tabControl1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 461);
            panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabOrderDetails);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(450, 461);
            tabControl1.TabIndex = 0;
            // 
            // tabOrderDetails
            // 
            tabOrderDetails.BackColor = Color.CornflowerBlue;
            tabOrderDetails.Controls.Add(dgvOrderDetails);
            tabOrderDetails.Controls.Add(listView1);
            tabOrderDetails.Controls.Add(panel4);
            tabOrderDetails.Location = new Point(4, 26);
            tabOrderDetails.Name = "tabOrderDetails";
            tabOrderDetails.Padding = new Padding(3);
            tabOrderDetails.Size = new Size(442, 431);
            tabOrderDetails.TabIndex = 0;
            tabOrderDetails.Text = "Order Details";
            // 
            // dgvOrderDetails
            // 
            dgvOrderDetails.AllowUserToAddRows = false;
            dgvOrderDetails.AllowUserToDeleteRows = false;
            dgvOrderDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderDetails.BackgroundColor = Color.LightBlue;
            dgvOrderDetails.ColumnHeadersHeight = 50;
            dgvOrderDetails.Columns.AddRange(new DataGridViewColumn[] { ProductID, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5 });
            dgvOrderDetails.Dock = DockStyle.Fill;
            dgvOrderDetails.Location = new Point(3, 3);
            dgvOrderDetails.MultiSelect = false;
            dgvOrderDetails.Name = "dgvOrderDetails";
            dgvOrderDetails.ReadOnly = true;
            dgvOrderDetails.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgvOrderDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderDetails.Size = new Size(436, 355);
            dgvOrderDetails.TabIndex = 4;
            // 
            // ProductID
            // 
            ProductID.DataPropertyName = "ProductID";
            ProductID.HeaderText = "ID";
            ProductID.Name = "ProductID";
            ProductID.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "ProductName";
            dataGridViewTextBoxColumn2.HeaderText = "Product Name";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "Price";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewTextBoxColumn3.HeaderText = "Unit Price";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "Quantity";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewTextBoxColumn4.HeaderText = "Quantity";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "TotalPrice";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.ForeColor = Color.Blue;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewTextBoxColumn5.HeaderText = "Sub Total";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // listView1
            // 
            listView1.BackColor = Color.LightBlue;
            listView1.Columns.AddRange(new ColumnHeader[] { ID, ProductName, Quantity, Price });
            listView1.Dock = DockStyle.Fill;
            listView1.Location = new Point(3, 3);
            listView1.Name = "listView1";
            listView1.Size = new Size(436, 355);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            // 
            // ID
            // 
            ID.Tag = "ID";
            ID.Text = "ID";
            // 
            // ProductName
            // 
            ProductName.Tag = "Product";
            ProductName.Text = "Product";
            // 
            // Quantity
            // 
            Quantity.Tag = "Quantity";
            Quantity.Text = "Qty";
            // 
            // Price
            // 
            Price.Tag = "Price";
            Price.Text = "Price";
            // 
            // panel4
            // 
            panel4.BackColor = Color.SteelBlue;
            panel4.Controls.Add(btnOrder);
            panel4.Controls.Add(txtTotalDetails);
            panel4.Controls.Add(label2);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(3, 358);
            panel4.Name = "panel4";
            panel4.Size = new Size(436, 70);
            panel4.TabIndex = 0;
            // 
            // btnOrder
            // 
            btnOrder.Anchor = AnchorStyles.Top;
            btnOrder.BackColor = Color.Navy;
            btnOrder.ForeColor = Color.AliceBlue;
            btnOrder.Location = new Point(331, 16);
            btnOrder.Name = "btnOrder";
            btnOrder.Size = new Size(86, 36);
            btnOrder.TabIndex = 9;
            btnOrder.Text = "Order";
            btnOrder.UseVisualStyleBackColor = false;
            btnOrder.Click += btnOrder_Click;
            // 
            // txtTotalDetails
            // 
            txtTotalDetails.Anchor = AnchorStyles.Top;
            txtTotalDetails.BackColor = Color.LightCyan;
            txtTotalDetails.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtTotalDetails.Location = new Point(110, 23);
            txtTotalDetails.Name = "txtTotalDetails";
            txtTotalDetails.Size = new Size(215, 25);
            txtTotalDetails.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.ForeColor = Color.AliceBlue;
            label2.Location = new Point(27, 26);
            label2.Name = "label2";
            label2.Size = new Size(77, 17);
            label2.TabIndex = 7;
            label2.Text = "Total Price:";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.CornflowerBlue;
            tabPage2.Controls.Add(panel6);
            tabPage2.Controls.Add(panel5);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(442, 431);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Order";
            // 
            // panel6
            // 
            panel6.BackColor = Color.LightBlue;
            panel6.Controls.Add(dgvOrder);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(3, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(436, 345);
            panel6.TabIndex = 9;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Top;
            btnPrint.BackColor = Color.Navy;
            btnPrint.ForeColor = Color.AliceBlue;
            btnPrint.Location = new Point(347, 41);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(86, 36);
            btnPrint.TabIndex = 14;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // dgvOrder
            // 
            dgvOrder.AllowUserToAddRows = false;
            dgvOrder.AllowUserToDeleteRows = false;
            dgvOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrder.BackgroundColor = Color.LightBlue;
            dgvOrder.ColumnHeadersHeight = 60;
            dgvOrder.Columns.AddRange(new DataGridViewColumn[] { OrderID, EmployeeName, CustomerName, Date, TotalPrice, ViewDetails });
            dgvOrder.Dock = DockStyle.Fill;
            dgvOrder.Location = new Point(0, 0);
            dgvOrder.MultiSelect = false;
            dgvOrder.Name = "dgvOrder";
            dgvOrder.ReadOnly = true;
            dgvOrder.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dgvOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrder.Size = new Size(436, 345);
            dgvOrder.TabIndex = 3;
            dgvOrder.CellContentClick += dgvOrder_CellContentClick;
            // 
            // OrderID
            // 
            OrderID.DataPropertyName = "ID";
            OrderID.HeaderText = "ID";
            OrderID.Name = "OrderID";
            OrderID.ReadOnly = true;
            // 
            // EmployeeName
            // 
            EmployeeName.DataPropertyName = "EmployeeName";
            EmployeeName.HeaderText = "Employee";
            EmployeeName.Name = "EmployeeName";
            EmployeeName.ReadOnly = true;
            // 
            // CustomerName
            // 
            CustomerName.DataPropertyName = "CustomerName";
            CustomerName.HeaderText = "Customer";
            CustomerName.Name = "CustomerName";
            CustomerName.ReadOnly = true;
            // 
            // Date
            // 
            Date.DataPropertyName = "Date";
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "dd/MM/yyyy";
            dataGridViewCellStyle9.NullValue = "dd/MM/yyyy";
            Date.DefaultCellStyle = dataGridViewCellStyle9;
            Date.HeaderText = "Create Date";
            Date.Name = "Date";
            Date.ReadOnly = true;
            // 
            // TotalPrice
            // 
            TotalPrice.DataPropertyName = "TotalPrice";
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle10.ForeColor = Color.Blue;
            dataGridViewCellStyle10.Format = "N0";
            TotalPrice.DefaultCellStyle = dataGridViewCellStyle10;
            TotalPrice.HeaderText = "Total Price";
            TotalPrice.Name = "TotalPrice";
            TotalPrice.ReadOnly = true;
            // 
            // ViewDetails
            // 
            ViewDetails.DataPropertyName = "ViewDetails";
            ViewDetails.HeaderText = "Details";
            ViewDetails.Name = "ViewDetails";
            ViewDetails.ReadOnly = true;
            ViewDetails.Text = "View Details";
            ViewDetails.UseColumnTextForLinkValue = true;
            // 
            // panel5
            // 
            panel5.BackColor = Color.SteelBlue;
            panel5.Controls.Add(btnPrint);
            panel5.Controls.Add(btnPay);
            panel5.Controls.Add(btnFilter);
            panel5.Controls.Add(dtpEnd);
            panel5.Controls.Add(dtpStart);
            panel5.Controls.Add(txtRevenue);
            panel5.Controls.Add(label3);
            panel5.Controls.Add(label4);
            panel5.Controls.Add(label1);
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(3, 348);
            panel5.Name = "panel5";
            panel5.Size = new Size(436, 80);
            panel5.TabIndex = 7;
            // 
            // btnPay
            // 
            btnPay.Anchor = AnchorStyles.Top;
            btnPay.BackColor = Color.DodgerBlue;
            btnPay.ForeColor = Color.AliceBlue;
            btnPay.Location = new Point(255, 41);
            btnPay.Name = "btnPay";
            btnPay.Size = new Size(86, 36);
            btnPay.TabIndex = 14;
            btnPay.Text = "Pay";
            btnPay.UseVisualStyleBackColor = false;
            btnPay.Click += btnPay_Click;
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Top;
            btnFilter.BackColor = Color.RoyalBlue;
            btnFilter.ForeColor = Color.AliceBlue;
            btnFilter.Location = new Point(347, 3);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(86, 36);
            btnFilter.TabIndex = 13;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Top;
            dtpEnd.CustomFormat = "MM/dd/yyyy";
            dtpEnd.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(232, 7);
            dtpEnd.MaxDate = new DateTime(9997, 12, 31, 0, 0, 0, 0);
            dtpEnd.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(109, 25);
            dtpEnd.TabIndex = 12;
            dtpEnd.Value = new DateTime(2025, 5, 25, 23, 59, 59, 0);
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Top;
            dtpStart.CustomFormat = "MM/dd/yyyy";
            dtpStart.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(52, 8);
            dtpStart.MaxDate = new DateTime(9997, 12, 31, 0, 0, 0, 0);
            dtpStart.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(109, 25);
            dtpStart.TabIndex = 12;
            dtpStart.Value = new DateTime(2025, 5, 25, 23, 59, 59, 0);
            // 
            // txtRevenue
            // 
            txtRevenue.Anchor = AnchorStyles.Top;
            txtRevenue.BackColor = Color.LightCyan;
            txtRevenue.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtRevenue.Location = new Point(77, 48);
            txtRevenue.Name = "txtRevenue";
            txtRevenue.Size = new Size(172, 25);
            txtRevenue.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.SteelBlue;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.ForeColor = Color.AliceBlue;
            label3.Location = new Point(7, 51);
            label3.Name = "label3";
            label3.Size = new Size(64, 17);
            label3.TabIndex = 9;
            label3.Text = "Revenue:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.SteelBlue;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label4.ForeColor = Color.AliceBlue;
            label4.Location = new Point(182, 13);
            label4.Name = "label4";
            label4.Size = new Size(35, 17);
            label4.TabIndex = 9;
            label4.Text = "End:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SteelBlue;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label1.ForeColor = Color.AliceBlue;
            label1.Location = new Point(5, 13);
            label1.Name = "label1";
            label1.Size = new Size(41, 17);
            label1.TabIndex = 9;
            label1.Text = "Start:";
            // 
            // panel2
            // 
            panel2.BackColor = Color.MidnightBlue;
            panel2.Controls.Add(txtFind);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(btnFind);
            panel2.Controls.Add(btnCancel);
            panel2.Controls.Add(btnClose);
            panel2.Controls.Add(bntDelete);
            panel2.Controls.Add(btnAdd);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(450, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(644, 100);
            panel2.TabIndex = 1;
            // 
            // txtFind
            // 
            txtFind.Anchor = AnchorStyles.Top;
            txtFind.BackColor = Color.LightCyan;
            txtFind.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            txtFind.Location = new Point(206, 16);
            txtFind.Name = "txtFind";
            txtFind.Size = new Size(252, 25);
            txtFind.TabIndex = 6;
            txtFind.TextChanged += txtFind_TextChanged;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label6.ForeColor = Color.AliceBlue;
            label6.Location = new Point(100, 19);
            label6.Name = "label6";
            label6.Size = new Size(91, 17);
            label6.TabIndex = 5;
            label6.Text = "Find product:";
            // 
            // btnFind
            // 
            btnFind.Anchor = AnchorStyles.Top;
            btnFind.AutoEllipsis = true;
            btnFind.BackColor = Color.LightSkyBlue;
            btnFind.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFind.Location = new Point(474, 7);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(80, 40);
            btnFind.TabIndex = 41;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = false;
            btnFind.Click += btnFind_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top;
            btnCancel.BackColor = Color.DarkTurquoise;
            btnCancel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnCancel.Location = new Point(356, 53);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 40);
            btnCancel.TabIndex = 40;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(474, 53);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 40);
            btnClose.TabIndex = 38;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // bntDelete
            // 
            bntDelete.Anchor = AnchorStyles.Top;
            bntDelete.BackColor = Color.SteelBlue;
            bntDelete.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            bntDelete.ForeColor = Color.AliceBlue;
            bntDelete.Location = new Point(238, 53);
            bntDelete.Name = "bntDelete";
            bntDelete.Size = new Size(80, 40);
            bntDelete.TabIndex = 37;
            bntDelete.Text = "Delete";
            bntDelete.UseVisualStyleBackColor = false;
            bntDelete.Click += bntDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top;
            btnAdd.BackColor = Color.DodgerBlue;
            btnAdd.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnAdd.ForeColor = Color.AliceBlue;
            btnAdd.Location = new Point(100, 53);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 40);
            btnAdd.TabIndex = 35;
            btnAdd.Text = "Add to cart";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(statusStrip1);
            panel3.Controls.Add(flowLayoutPanel1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(450, 100);
            panel3.Name = "panel3";
            panel3.Size = new Size(644, 361);
            panel3.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.DarkTurquoise;
            statusStrip1.Items.AddRange(new ToolStripItem[] { sttEmployee });
            statusStrip1.Location = new Point(0, 339);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(644, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // sttEmployee
            // 
            sttEmployee.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sttEmployee.Name = "sttEmployee";
            sttEmployee.Size = new Size(0, 17);
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.SkyBlue;
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(10, 3, 3, 10);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0, 0, 0, 10);
            flowLayoutPanel1.Size = new Size(644, 361);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // frmSale
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1094, 461);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmSale";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Saller";
            WindowState = FormWindowState.Maximized;
            Load += Saller_Load;
            panel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabOrderDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            tabPage2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvOrder).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TabControl tabControl1;
        private TabPage tabOrderDetails;
        private TabPage tabPage2;
        private Panel panel2;
        private Panel panel3;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnClose;
        private Button bntDelete;
        private Button btnAdd;
        private Panel panel4;
        private Button btnCancel;
        private Button btnFind;
        private TextBox txtFind;
        private Label label6;
        private ListView listView1;
        private ColumnHeader ID;
        private ColumnHeader ProductName;
        private ColumnHeader Quantity;
        private ColumnHeader Price;
        private Panel panel6;
        private Panel panel5;
        private TextBox txtTotalDetails;
        private Label label2;
        private DataGridView dgvOrderDetails;
        private DataGridViewTextBoxColumn ProductID;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private Label label1;
        private TextBox txtRevenue;
        private Label label3;
        private DateTimePicker dtpStart;
        private Button btnFilter;
        private Button btnOrder;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel sttEmployee;
        private DateTimePicker dtpEnd;
        private Label label4;
        private HelpProvider helpProvider1;
        private Button btnPrint;
        public DataGridView dgvOrder;
        private DataGridViewTextBoxColumn OrderID;
        private DataGridViewTextBoxColumn EmployeeName;
        private DataGridViewTextBoxColumn CustomerName;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn TotalPrice;
        private DataGridViewLinkColumn ViewDetails;
        private Button btnPay;
    }
}