using System;
using System.ComponentModel.DataAnnotations;

namespace ElectronicsStore.DataAccess
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string Changes { get; set; }
        public string Username { get; set; }
    }
}
