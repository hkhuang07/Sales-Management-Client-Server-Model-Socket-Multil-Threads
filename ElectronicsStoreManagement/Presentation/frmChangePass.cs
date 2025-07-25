using ElectronicsStore.Client; // <-- Quan trọng: Thêm namespace của ClientService
using ElectronicsStore.DataTransferObject;
using System;
using System.Configuration;
using System.Windows.Forms;
using Newtonsoft.Json; // Vẫn cần nếu có xử lý JSON độc lập, nhưng ClientService đã xử lý phần lớn

namespace ElectronicsStore.Presentation
{
    public partial class frmChangePass : Form
    {
        private readonly int _loggedInEmployeeId; // ID of the logged-in employee
        private readonly string _loggedInUsername; // Username of the logged-in employee

        private readonly ClientService _clientService; // Giữ lại ClientService

        // Constructor đã nhận ClientService qua dependency injection, rất tốt!
        public frmChangePass(int employeeId, string username, ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
            _loggedInEmployeeId = employeeId;
            _loggedInUsername = username;
            _clientService = clientService; // Gán instance ClientService đã được truyền vào

            // Hiển thị username (nếu có control txtUsername trên form)
            // if (txtUsername != null) // Đảm bảo control tồn tại
            // {
            //     txtUsername.Text = _loggedInUsername;
            //     txtUsernasme.ReadOnly = true;
            // }

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString() ?? "";
            helpProvider1.HelpNamespace = helpURL + "changepassword.html";
        }

        private void frmChangePass_Load(object sender, EventArgs e)
        {
            // Set focus to the old password field
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

            // You might want to add more client-side validation for new password strength
            if (newPassword.Length < 6) // Example: minimum 6 characters
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
                // Chuẩn bị dữ liệu cho request
                var changePasswordRequestData = new ChangePasswordRequestDTO
                {
                    EmployeeId = _loggedInEmployeeId,
                    OldPassword = oldPassword,
                    NewPassword = newPassword
                };

                // Gửi request đến server bằng ClientService
                // ClientService.SendRequest sẽ trả về 'bool' (TResponseData) nếu thành công
                // hoặc ném Exception nếu server trả về Success = false hoặc có lỗi mạng
                bool success = await _clientService.SendRequest<ChangePasswordRequestDTO, bool>("ChangeEmployeePassword", changePasswordRequestData);

                // Nếu không có exception, nghĩa là thao tác thành công.
                if (success)
                {
                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form khi đổi mật khẩu thành công
                }
                else
                {
                    // Trường hợp này chỉ xảy ra nếu server trả về ServerResponse.Data = false mà không ném lỗi.
                    // Tuy nhiên, theo logic ClientService hiện tại, nếu server trả về Success = false, ClientService sẽ ném Exception.
                    // Do đó, dòng này có thể ít khi được chạy nếu logic server đúng.
                    MessageBox.Show("Password change failed. Please check your old password or contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Mọi lỗi từ ClientService (bao gồm cả lỗi từ serverResponse.Message nếu Success là false
                // và lỗi kết nối mạng, JSON) đều sẽ được bắt ở đây.
                MessageBox.Show($"Error during password change: {ex.Message}", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}