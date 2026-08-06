using System;
using System.Windows.Forms;
using ElectronicsStore.Client;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.Presentation
{
    public partial class frmSignUp : Form
    {
        private readonly ClientService _clientService;

        public frmSignUp(ClientService clientService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            InitializeComponent();
        }

        private async void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirm.Text;
            string fullName = txtFullName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Please fill in Username, Password, and Full Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSignUp.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                var registerRequest = new RegisterRequestDTO
                {
                    Username = username,
                    Password = password,
                    FullName = fullName,
                    EmployeePhone = phone,
                    EmployeeAddress = address,
                    Role = false // Staff by default
                };

                bool success = await _clientService.RegisterAsync(registerRequest);
                if (success)
                {
                    MessageBox.Show("Account registered successfully! You can now log in.", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try a different username or check input fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Registration error: {ex.Message}", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSignUp.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
