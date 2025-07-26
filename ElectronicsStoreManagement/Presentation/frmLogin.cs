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
            btnLogin.Enabled = false;
            btnCancel.Enabled = false;

            try
            {
                string username = txtUserName.Text.Trim();
                string password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Please enter both username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var loginRequest = new LoginRequestDTO
                {
                    Username = username,
                    Password = password
                };

                // GỌI THẲNG AuthenticateUser, nó sẽ trả về LoginResponseDTO hoặc ném exception
                LoggedInUser = await _clientService.AuthenticateEmployee(loginRequest);

                // Nếu không có exception, nghĩa là đăng nhập thành công
                if (LoggedInUser != null) // Đảm bảo dữ liệu không null (mặc dù AuthenticateUser sẽ ném nếu không có dữ liệu)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Trường hợp này có thể xảy ra nếu server trả về Success=true nhưng Data=null,
                    // mà trong logic AuthenticateUser đã được xử lý để trả về default(TResponseData) là null cho kiểu tham chiếu.
                    MessageBox.Show("Login failed. Received empty user data.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                // Mọi lỗi từ ClientService (kết nối, timeout, server báo lỗi, JSON lỗi)
                // đều sẽ được ném ra và bắt tại đây.
                MessageBox.Show($"Login failed: {ex.Message}", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
            finally
            {
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