namespace ElectronicsStore.Presentation
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            chkSave = new CheckBox();
            btnCancel = new Button();
            btnLogin = new Button();
            txtPassword = new TextBox();
            txtUserName = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            helpProvider1 = new HelpProvider();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Left;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(287, 361);
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightBlue;
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(chkSave);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtUserName);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(287, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(247, 361);
            panel1.TabIndex = 0;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(165, 42);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(70, 69);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // chkSave
            // 
            chkSave.AutoSize = true;
            chkSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSave.ForeColor = Color.MidnightBlue;
            chkSave.Location = new Point(9, 226);
            chkSave.Name = "chkSave";
            chkSave.Size = new Size(152, 19);
            chkSave.TabIndex = 3;
            chkSave.Text = "Save login information";
            chkSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.CornflowerBlue;
            btnCancel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancel.ForeColor = Color.AliceBlue;
            btnCancel.Location = new Point(149, 277);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 40);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.RoyalBlue;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.AliceBlue;
            btnLogin.Location = new Point(9, 277);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(86, 40);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Log In";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtPassword.Location = new Point(9, 191);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(229, 29);
            txtPassword.TabIndex = 2;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // txtUserName
            // 
            txtUserName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            txtUserName.Location = new Point(9, 126);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(229, 29);
            txtUserName.TabIndex = 1;
            txtUserName.KeyDown += txtUserName_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.MidnightBlue;
            label3.Location = new Point(9, 161);
            label3.Name = "label3";
            label3.Size = new Size(86, 21);
            label3.TabIndex = 0;
            label3.Text = "Password:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.ForeColor = Color.MidnightBlue;
            label2.Location = new Point(9, 98);
            label2.Name = "label2";
            label2.Size = new Size(91, 21);
            label2.TabIndex = 0;
            label2.Text = "Username:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.LightCyan;
            label1.Font = new Font("Segoe UI Black", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.MidnightBlue;
            label1.Location = new Point(9, 42);
            label1.Name = "label1";
            label1.Size = new Size(98, 37);
            label1.TabIndex = 0;
            label1.Text = "Log In";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SkyBlue;
            ClientSize = new Size(534, 361);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            helpProvider1.SetHelpKeyword(this, "F1");
            Name = "frmLogin";
            helpProvider1.SetShowHelp(this, true);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += frmLogin_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private Panel panel1;
        private Button btnCancel;
        private Button btnLogin;
        private Label label3;
        private Label label2;
        private Label label1;
        public TextBox txtPassword;
        public TextBox txtUserName;
        private PictureBox pictureBox2;
        public CheckBox chkSave;
        private HelpProvider helpProvider1;
    }
}