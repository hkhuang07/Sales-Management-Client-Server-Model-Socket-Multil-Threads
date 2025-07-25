using System;
using System.Configuration;
using System.Windows.Forms;
using ElectronicsStore.Client; // Đảm bảo namespace này đúng cho ClientService
using ElectronicsStore.DataTransferObject; // Đảm bảo namespace này đúng cho LoginRequestDTO và ServerResponse<T>

namespace ElectronicsStore.Presentation
{
    public partial class frmLogin : Form
    {
        private readonly ClientService _clientService;

        // Public property to hold logged-in user info if successful
        public LoginResponseDTO LoggedInUser { get; private set; }

        public frmLogin(ClientService clientService)
        {
            _clientService = clientService;

            InitializeComponent();
            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString() ?? string.Empty; // Xử lý null
            helpProvider1.HelpNamespace = helpURL + "login.html";
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Disable buttons to prevent multiple clicks while processing
            btnLogin.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                string username = txtUserName.Text.Trim();
                string password = txtPassword.Text; // Không trim password

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create the request DTO
                var loginRequest = new LoginRequestDTO
                {
                    Username = username,
                    Password = password
                };

                // Send the login request to the server
                // We expect ServerResponse<LoginResponseDTO> back
                ServerResponse<LoginResponseDTO> response =
                    await _clientService.SendRequest<LoginRequestDTO, ServerResponse<LoginResponseDTO>>("AuthenticateUser", loginRequest);

                if (response.Success && response.Data != null)
                {
                    // Login successful
                    LoggedInUser = response.Data; // Store user info
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Login failed
                    MessageBox.Show(response.Message ?? "Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear(); // Clear password field for security
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                // Handle connection errors or other unexpected exceptions
                MessageBox.Show($"Error connecting to server or processing login: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable buttons
                btnLogin.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Initial load logic if any
        }
    }
}   