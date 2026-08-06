using System;

namespace ElectronicsStore.DataTransferObject
{
    public class RegisterRequestDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string EmployeePhone { get; set; } = string.Empty;
        public string EmployeeAddress { get; set; } = string.Empty;
        public bool Role { get; set; } = false; // Default: false = Staff, true = Admin
    }
}
