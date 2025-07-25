using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? EmployeePhone { get; set; } = string.Empty;
        public string? EmployeeAddress { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        // Để Password nullable (khi không cập nhật) nhưng vẫn rõ ràng về ý định
        public string Password { get; set; }
        public bool Role { get; set; } // true = Admin, false = Staff
    }
}
