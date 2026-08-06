namespace ElectronicsStore.Presentation
{
    partial class frmSignUp
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            labelTitle = new Label();
            lblUserName = new Label();
            txtUserName = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblConfirm = new Label();
            txtConfirm = new TextBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            btnSignUp = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            labelTitle.ForeColor = Color.MidnightBlue;
            labelTitle.Location = new Point(140, 15);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(218, 32);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Employee Sign Up";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUserName.ForeColor = Color.MidnightBlue;
            lblUserName.Location = new Point(30, 65);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(81, 19);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Username:";
            // 
            // txtUserName
            // 
            txtUserName.Font = new Font("Segoe UI", 10F);
            txtUserName.Location = new Point(160, 62);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(250, 25);
            txtUserName.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.MidnightBlue;
            lblPassword.Location = new Point(30, 105);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(77, 19);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(160, 102);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(250, 25);
            txtPassword.TabIndex = 4;
            // 
            // lblConfirm
            // 
            lblConfirm.AutoSize = true;
            lblConfirm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblConfirm.ForeColor = Color.MidnightBlue;
            lblConfirm.Location = new Point(30, 145);
            lblConfirm.Name = "lblConfirm";
            lblConfirm.Size = new Size(135, 19);
            lblConfirm.TabIndex = 5;
            lblConfirm.Text = "Confirm Password:";
            // 
            // txtConfirm
            // 
            txtConfirm.Font = new Font("Segoe UI", 10F);
            txtConfirm.Location = new Point(160, 142);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PasswordChar = '●';
            txtConfirm.Size = new Size(250, 25);
            txtConfirm.TabIndex = 6;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFullName.ForeColor = Color.MidnightBlue;
            lblFullName.Location = new Point(30, 185);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(82, 19);
            lblFullName.TabIndex = 7;
            lblFullName.Text = "Full Name:";
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.Location = new Point(160, 182);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(250, 25);
            txtFullName.TabIndex = 8;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhone.ForeColor = Color.MidnightBlue;
            lblPhone.Location = new Point(30, 225);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(57, 19);
            lblPhone.TabIndex = 9;
            lblPhone.Text = "Phone:";
            // 
            // txtPhone
            // 
            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.Location = new Point(160, 222);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(250, 25);
            txtPhone.TabIndex = 10;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAddress.ForeColor = Color.MidnightBlue;
            lblAddress.Location = new Point(30, 265);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(67, 19);
            lblAddress.TabIndex = 11;
            lblAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            txtAddress.Font = new Font("Segoe UI", 10F);
            txtAddress.Location = new Point(160, 262);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(250, 25);
            txtAddress.TabIndex = 12;
            // 
            // btnSignUp
            // 
            btnSignUp.BackColor = Color.RoyalBlue;
            btnSignUp.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSignUp.ForeColor = Color.AliceBlue;
            btnSignUp.Location = new Point(160, 310);
            btnSignUp.Name = "btnSignUp";
            btnSignUp.Size = new Size(115, 38);
            btnSignUp.TabIndex = 13;
            btnSignUp.Text = "Sign Up";
            btnSignUp.UseVisualStyleBackColor = false;
            btnSignUp.Click += btnSignUp_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.CornflowerBlue;
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.AliceBlue;
            btnCancel.Location = new Point(295, 310);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(115, 38);
            btnCancel.TabIndex = 14;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // frmSignUp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightBlue;
            ClientSize = new Size(460, 375);
            Controls.Add(btnCancel);
            Controls.Add(btnSignUp);
            Controls.Add(txtAddress);
            Controls.Add(lblAddress);
            Controls.Add(txtPhone);
            Controls.Add(lblPhone);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(txtConfirm);
            Controls.Add(lblConfirm);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUserName);
            Controls.Add(lblUserName);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSignUp";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sign Up - Employee Account";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelTitle;
        private Label lblUserName;
        private TextBox txtUserName;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirm;
        private TextBox txtConfirm;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblAddress;
        private TextBox txtAddress;
        private Button btnSignUp;
        private Button btnCancel;
    }
}
