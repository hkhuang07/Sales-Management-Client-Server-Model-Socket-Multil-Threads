using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ElectronicsStore.DataAccess
{
    public class Employees
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string? EmployeePhone { get; set; }
        public string? EmployeeAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Role { get; set; }
        public virtual ObservableCollectionListSource<Orders> Order { get; } = new();
    }
}
