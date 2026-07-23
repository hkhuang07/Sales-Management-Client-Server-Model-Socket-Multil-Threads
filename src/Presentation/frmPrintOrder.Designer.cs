namespace ElectronicsStore.Presentation
{
    partial class frmPrintOrder
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
            reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            helpProvider1 = new HelpProvider();
            panel1 = new Panel();
            btnClose = new Button();
            btnPrint = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // reportViewer
            // 
            reportViewer.BackColor = Color.LightBlue;
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.Location = new Point(0, 0);
            reportViewer.Name = "reportViewer";
            reportViewer.ServerReport.BearerToken = null;
            reportViewer.Size = new Size(684, 461);
            reportViewer.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.SkyBlue;
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnPrint);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 407);
            panel1.Name = "panel1";
            panel1.Size = new Size(684, 54);
            panel1.TabIndex = 2;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top;
            btnClose.BackColor = Color.Turquoise;
            btnClose.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            btnClose.Location = new Point(597, 13);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 29);
            btnClose.TabIndex = 14;
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
            btnPrint.Location = new Point(492, 13);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(99, 29);
            btnPrint.TabIndex = 9;
            btnPrint.Text = "Print Order";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // frmPrintOrder
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(panel1);
            Controls.Add(reportViewer);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmPrintOrder";
            helpProvider1.SetShowHelp(this, true);
            Text = "Print Order";
            Load += frmPrintOrder_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private HelpProvider helpProvider1;
        private Panel panel1;
        private Button btnPrint;
        private Button btnClose;
    }
}