using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class LoginResponseDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public bool Roles { get; set; }
        public string Token { get; set; } // JWT token nếu sử dụng
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
