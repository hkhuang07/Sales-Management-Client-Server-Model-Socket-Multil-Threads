namespace ElectronicsStore.Presentation
{
    partial class frmChangePass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePass));
            panel1 = new Panel();
            label5 = new Label();
            btnCancel = new Button();
            btnLogin = new Button();
            txtNewPass = new TextBox();
            txtConfirm = new TextBox();
            txtOldPass = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            helpProvider1 = new HelpProvider();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightBlue;
            panel1.Controls.Add(label5);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(txtNewPass);
            panel1.Controls.Add(txtConfirm);
            panel1.Controls.Add(txtOldPass);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(223, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(311, 361);
            panel1.TabIndex = 2;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label5.ForeColor = Color.MidnightBlue;
            label5.Location = new Point(32, 98);
            label5.Name = "label5";
            label5.Size = new Size(117, 21);
            label5.TabIndex = 6;
            label5.Text = "Old Password:";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top;
            btnCancel.BackColor = Color.CornflowerBlue;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.AliceBlue;
            btnCancel.Location = new Point(216, 309);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top;
            btnLogin.BackColor = Color.RoyalBlue;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.AliceBlue;
            btnLogin.Location = new Point(9, 309);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(86, 40);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Confirm";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnChange_Click;
            // 
            // txtNewPass
            // 
            txtNewPass.Anchor = AnchorStyles.Top;
            txtNewPass.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtNewPass.Location = new Point(32, 186);
            txtNewPass.Name = "txtNewPass";
            txtNewPass.PasswordChar = '●';
            txtNewPass.Size = new Size(253, 29);
            txtNewPass.TabIndex = 2;
            // 
            // txtConfirm
            // 
            txtConfirm.Anchor = AnchorStyles.Top;
            txtConfirm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtConfirm.Location = new Point(32, 242);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PasswordChar = '●';
            txtConfirm.Size = new Size(253, 29);
            txtConfirm.TabIndex = 2;
            // 
            // txtOldPass
            // 
            txtOldPass.Anchor = AnchorStyles.Top;
            txtOldPass.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtOldPass.Location = new Point(32, 126);
            txtOldPass.Name = "txtOldPass";
            txtOldPass.PasswordChar = '●';
            txtOldPass.Size = new Size(253, 29);
            txtOldPass.TabIndex = 1;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label4.ForeColor = Color.MidnightBlue;
            label4.Location = new Point(32, 158);
            label4.Name = "label4";
            label4.Size = new Size(125, 21);
            label4.TabIndex = 0;
            label4.Text = "New Password:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.MidnightBlue;
            label3.Location = new Point(32, 218);
            label3.Name = "label3";
            label3.Size = new Size(152, 21);
            label3.TabIndex = 0;
            label3.Text = "Confirm Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.ForeColor = Color.MidnightBlue;
            label2.Location = new Point(9, 98);
            label2.Name = "label2";
            label2.Size = new Size(0, 21);
            label2.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.BackColor = Color.LightCyan;
            label1.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(32, 45);
            label1.Name = "label1";
            label1.Size = new Size(253, 37);
            label1.TabIndex = 0;
            label1.Text = "Change Password";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.DodgerBlue;
            pictureBox1.Dock = DockStyle.Left;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(223, 361);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // frmChangePass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 361);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmChangePass";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Change Password";
            Load += frmChangePass_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btnCancel;
        private Button btnLogin;
        public TextBox txtConfirm;
        public TextBox txtOldPass;
        private Label label3;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;
        public TextBox txtNewPass;
        private Label label4;
        private Label label5;
        private HelpProvider helpProvider1;
    }
}