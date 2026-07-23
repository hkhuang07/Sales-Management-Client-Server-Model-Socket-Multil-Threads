namespace ElectronicsStore.Presentation
{
    partial class frmRevenueStatistics
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
            dtpEnd = new DateTimePicker();
            dtpStart = new DateTimePicker();
            btnShowAll = new Button();
            btnFilter = new Button();
            label3 = new Label();
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
            panel1.Controls.Add(dtpEnd);
            panel1.Controls.Add(dtpStart);
            panel1.Controls.Add(btnShowAll);
            panel1.Controls.Add(btnFilter);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(684, 125);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(565, 89);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(96, 29);
            btnClose.TabIndex = 18;
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
            btnPrint.Location = new Point(460, 89);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(99, 30);
            btnPrint.TabIndex = 17;
            btnPrint.Text = "Print Order";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Top;
            dtpEnd.CustomFormat = "MM/dd/yyyy";
            dtpEnd.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.Location = new Point(325, 57);
            dtpEnd.MaxDate = new DateTime(9997, 12, 31, 0, 0, 0, 0);
            dtpEnd.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(101, 25);
            dtpEnd.TabIndex = 11;
            dtpEnd.Value = new DateTime(2025, 5, 25, 23, 59, 59, 0);
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Top;
            dtpStart.CustomFormat = "MM/dd/yyyy";
            dtpStart.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.Location = new Point(112, 57);
            dtpStart.MaxDate = new DateTime(9997, 12, 31, 0, 0, 0, 0);
            dtpStart.MinDate = new DateTime(2020, 1, 1, 0, 0, 0, 0);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(101, 25);
            dtpStart.TabIndex = 11;
            dtpStart.Value = new DateTime(2025, 5, 25, 23, 59, 59, 0);
            // 
            // btnShowAll
            // 
            btnShowAll.Anchor = AnchorStyles.Top;
            btnShowAll.BackColor = Color.SteelBlue;
            btnShowAll.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnShowAll.ForeColor = Color.AliceBlue;
            btnShowAll.Location = new Point(565, 51);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(96, 31);
            btnShowAll.TabIndex = 10;
            btnShowAll.Text = "Show All";
            btnShowAll.UseVisualStyleBackColor = false;
            btnShowAll.Click += btnShowAll_Click;
            // 
            // btnFilter
            // 
            btnFilter.Anchor = AnchorStyles.Top;
            btnFilter.BackColor = Color.RoyalBlue;
            btnFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnFilter.ForeColor = Color.AliceBlue;
            btnFilter.Location = new Point(460, 53);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(99, 31);
            btnFilter.TabIndex = 10;
            btnFilter.Text = "Filter";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label3.Location = new Point(261, 61);
            label3.Name = "label3";
            label3.Size = new Size(58, 17);
            label3.TabIndex = 6;
            label3.Text = "To date:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            label2.Location = new Point(31, 61);
            label2.Name = "label2";
            label2.Size = new Size(75, 17);
            label2.TabIndex = 7;
            label2.Text = "Form date:";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.LightCyan;
            label1.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(214, 9);
            label1.Name = "label1";
            label1.Size = new Size(257, 37);
            label1.TabIndex = 2;
            label1.Text = "Revenue Statistics";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(reportViewer);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 125);
            panel2.Name = "panel2";
            panel2.Size = new Size(684, 336);
            panel2.TabIndex = 4;
            // 
            // reportViewer
            // 
            reportViewer.BackColor = Color.LightBlue;
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.Location = new Point(0, 0);
            reportViewer.Name = "reportViewer";
            reportViewer.ServerReport.BearerToken = null;
            reportViewer.Size = new Size(684, 336);
            reportViewer.TabIndex = 4;
            // 
            // frmRevenueStatistics
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(panel2);
            Controls.Add(panel1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmRevenueStatistics";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Revenue Statistics";
            WindowState = FormWindowState.Maximized;
            Load += frmRevenueStatistics_Load_1;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private Button btnShowAll;
        private Button btnFilter;
        private Label label3;
        private Label label2;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private HelpProvider helpProvider1;
        private Button btnClose;
        private Button btnPrint;
    }
}