using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class ChangePasswordRequestDTO
    {
        public int EmployeeId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
