namespace ElectronicsStore.Presentation
{
    partial class frmProductStatistics
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
            panel1 = new Panel();
            btnClose = new Button();
            btnPrint = new Button();
            btnFilter = new Button();
            cboCategory = new ComboBox();
            label3 = new Label();
            cboManufacturer = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            helpProvider1 = new HelpProvider();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.SkyBlue;
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnPrint);
            panel1.Controls.Add(btnFilter);
            panel1.Controls.Add(cboCategory);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(cboManufacturer);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(684, 132);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(594, 100);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 29);
            btnClose.TabIndex = 16;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Top;
            btnPrint.BackColor = Color.Navy;
            btnPrint.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnPrint.ForeColor = Color.AliceBlue;
            btnPrint.Location = new Point(570, 56);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(99, 30);
            btnPrint.TabIndex = 15;
            btnPrint.Text = "Print Order";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Top;
            btnFilter.BackColor = Color.RoyalBlue;
            btnFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFilter.ForeColor = Color.AliceBlue;
            btnFilter.Location = new Point(481, 56);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(83, 29);
            btnFilter.TabIndex = 5;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // cboCategory
            // 
            cboCategory.Anchor = AnchorStyles.Top;
            cboCategory.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(336, 56);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(139, 25);
            cboCategory.TabIndex = 4;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(262, 59);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 3;
            label3.Text = "Category:";
            // 
            // cboManufacturer
            // 
            cboManufacturer.Anchor = AnchorStyles.Top;
            cboManufacturer.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            cboManufacturer.FormattingEnabled = true;
            cboManufacturer.Location = new Point(117, 56);
            cboManufacturer.Name = "cboManufacturer";
            cboManufacturer.Size = new Size(139, 25);
            cboManufacturer.TabIndex = 4;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(15, 59);
            label2.Name = "label2";
            label2.Size = new Size(96, 17);
            label2.TabIndex = 3;
            label2.Text = "Manufacturer:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.LightCyan;
            label1.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(218, 9);
            label1.Name = "label1";
            label1.Size = new Size(248, 37);
            label1.TabIndex = 2;
            label1.Text = "Product Statistics";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(reportViewer);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 132);
            panel2.Name = "panel2";
            panel2.Size = new Size(684, 329);
            panel2.TabIndex = 1;
            // 
            // reportViewer
            // 
            reportViewer.BackColor = Color.LightBlue;
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.Location = new Point(0, 0);
            reportViewer.Name = "reportViewer";
            reportViewer.ServerReport.BearerToken = null;
            reportViewer.Size = new Size(684, 329);
            reportViewer.TabIndex = 0;
            // 
            // frmProductStatistics
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(panel2);
            Controls.Add(panel1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmProductStatistics";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Product Statistics";
            WindowState = FormWindowState.Maximized;
            Load += frmProductStatistics_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Label label1;
        private Button button1;
        private ComboBox cboCategory;
        private Label label3;
        private ComboBox cboManufacturer;
        private Label label2;
        private Button btnFilter;
        private HelpProvider helpProvider1;
        private Button btnClose;
        private Button btnPrint;
    }
}