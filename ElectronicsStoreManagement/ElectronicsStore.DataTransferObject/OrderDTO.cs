using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataTransferObject
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public decimal TotalPrice { get; set; }
        public string? EmployeeName { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerPhone { get; set; }

    }
    public class ConfirmOrderDTO
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public string Note { get; set; }
        public bool PrintInvoice { get; set; }
    }

}
