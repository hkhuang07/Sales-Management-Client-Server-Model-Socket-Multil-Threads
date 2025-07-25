using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class LoginRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

        //public bool Role { get; set; } // true = Admin, false = Staff
    }
}
