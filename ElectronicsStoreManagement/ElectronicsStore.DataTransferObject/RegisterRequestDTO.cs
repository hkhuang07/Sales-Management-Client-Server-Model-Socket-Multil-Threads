using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class RegisterRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        // Có thể thêm Role mặc định nếu là đăng ký người dùng thông thường
    }

    public class ConfirmAccountRequestDTO
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }

    public class ResetPasswordRequestDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

    public class EmailRequestDTO
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}
