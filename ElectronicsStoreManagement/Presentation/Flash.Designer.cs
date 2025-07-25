namespace ElectronicsStore.Presentation
{
    partial class Flash
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Flash));
            pictureBox1 = new PictureBox();
            progressBar = new ProgressBar();
            timer = new System.Windows.Forms.Timer(components);
            helpProvider1 = new HelpProvider();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-26, -3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(644, 231);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top;
            progressBar.BackColor = Color.MidnightBlue;
            progressBar.ForeColor = Color.SkyBlue;
            progressBar.Location = new Point(105, 178);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(376, 23);
            progressBar.TabIndex = 1;
            // 
            // timer
            // 
            timer.Tick += timer_Tick;
            // 
            // Flash
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 225);
            Controls.Add(progressBar);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "Flash";
            helpProvider1.SetShowHelp(this, true);
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Flash";
            Load += Flash_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private ProgressBar progressBar;
        private System.Windows.Forms.Timer timer;
        private HelpProvider helpProvider1;
    }
}