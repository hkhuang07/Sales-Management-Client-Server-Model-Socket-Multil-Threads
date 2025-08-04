using ElectronicsStore.Client;
using ElectronicsStore.DataTransferObject;
using System;
using System.Configuration;
using System.Windows.Forms;
using BCrypt.Net; // Thêm namespace này nếu bạn dùng BCrypt để hash pass

namespace ElectronicsStore.Presentation
{
    public partial class frmChangePass : Form
    {
        private readonly int _loggedInEmployeeId;
        private readonly string _loggedInUsername;
        private readonly ClientService _clientService;

        // Constructor đã nhận ClientService qua dependency injection, rất tốt!
        public frmChangePass(int employeeId, string username, ClientService clientService)
        {
            InitializeComponent();
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            _loggedInEmployeeId = employeeId;
            _loggedInUsername = username;

            // Hiển thị username (nếu có control txtUsername trên form)
            // if (txtUsername != null) 
            // {
            //     txtUsername.Text = _loggedInUsername;
            //     txtUsername.ReadOnly = true;
            // }

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString() ?? "";
            helpProvider1.HelpNamespace = helpURL + "changepassword.html";
        }

        private void frmChangePass_Load(object sender, EventArgs e)
        {
            txtOldPass.Focus();
        }

        private async void btnChange_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPass.Text.Trim();
            string newPassword = txtNewPass.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();

            // Client-side validation
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New passwords do not match!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("New password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (oldPassword == newPassword)
            {
                MessageBox.Show("New password cannot be the same as the old password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo một DTO để gửi yêu cầu đổi mật khẩu
                var changePasswordRequestData = new ChangePasswordRequestDTO
                {
                    EmployeeId = _loggedInEmployeeId,
                    OldPassword = oldPassword,
                    NewPassword = newPassword
                };

                // Gửi request đến server bằng ClientService
                // Method 'ChangeEmployeePassword' sẽ được xử lý trên server
                bool success = await _clientService.SendRequest<ChangePasswordRequestDTO, bool>("ChangeEmployeePassword", changePasswordRequestData);

                if (success)
                {
                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Đặt DialogResult để thông báo thành công
                    this.Close();
                }
                else
                {
                    // Trường hợp này có thể không cần thiết nếu server luôn ném exception khi lỗi
                    MessageBox.Show("Password change failed. Please check your old password or contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Mọi lỗi từ ClientService sẽ được bắt ở đây
                MessageBox.Show($"Error during password change: {ex.Message}", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}