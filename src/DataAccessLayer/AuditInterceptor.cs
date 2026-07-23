using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicsStore.DataAccess
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is not null)
            {
                var auditEntries = CreateAuditEntries(eventData.Context);
                if (auditEntries.Any())
                {
                    eventData.Context.AddRange(auditEntries);
                }
            }
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                var auditEntries = CreateAuditEntries(eventData.Context);
                if (auditEntries.Any())
                {
                    eventData.Context.AddRange(auditEntries);
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private List<AuditLog> CreateAuditEntries(DbContext context)
        {
            var auditEntries = new List<AuditLog>();
            
            // Lấy tên người dùng hiện tại từ token JWT
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "System";

            var entries = context.ChangeTracker.Entries()
                .Where(e => e.Entity is not AuditLog && 
                            (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
                .ToList();

            foreach (var entry in entries)
            {
                var auditLog = new AuditLog
                {
                    EntityName = entry.Entity.GetType().Name,
                    Username = username,
                    Timestamp = DateTime.UtcNow,
                    Action = entry.State.ToString()
                };

                // Lấy thông tin thay đổi
                var changes = new Dictionary<string, object>();
                
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary) continue;
                    
                    if (entry.State == EntityState.Added)
                    {
                        changes[property.Metadata.Name] = property.CurrentValue;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        if (property.IsModified)
                        {
                            changes[property.Metadata.Name] = new
                            {
                                Old = property.OriginalValue,
                                New = property.CurrentValue
                            };
                        }
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        changes[property.Metadata.Name] = property.OriginalValue;
                    }
                }
                
                auditLog.Changes = JsonConvert.SerializeObject(changes);
                auditEntries.Add(auditLog);
            }

            return auditEntries;
        }
    }
}
