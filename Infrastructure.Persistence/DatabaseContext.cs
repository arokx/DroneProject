using Camms.Strategy.Infrastructure.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public override int SaveChanges()
        {
            AuditDeltaObject(string.Empty);
            var result = base.SaveChanges();

            AuditFullObject(string.Empty);
            base.SaveChanges();
            return result;
        }

        public virtual int SaveChanges(string userId = null)
        {
            AuditDeltaObject(userId);
            var result = base.SaveChanges();

            AuditFullObject(userId);
            base.SaveChanges();
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AuditDeltaObject(string.Empty);
            var result = await base.SaveChangesAsync(cancellationToken);

            AuditFullObject(string.Empty);
            await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        public virtual async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = new CancellationToken())
        {
            AuditDeltaObject(userId);
            var result = await base.SaveChangesAsync(cancellationToken);

            AuditFullObject(userId);
            await base.SaveChangesAsync(cancellationToken);
            return result;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        private void AuditDeltaObject(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog
                    || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || entry.State == EntityState.Added)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.PrimaryKey = int.TryParse(property.CurrentValue.ToString(), out int primaryKey) ? primaryKey : 0;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified && property.OriginalValue.ToString() != property.CurrentValue.ToString())
                            {
                                auditEntry.ChangedColumns.Add(propertyName);

                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLog.Add(auditEntry.ToAudit());
            }
        }

        private void AuditFullObject(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog
                    || entry.State == EntityState.Detached || entry.State == EntityState.Added
                    || entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.PrimaryKey = int.TryParse(property.CurrentValue.ToString(), out int primaryKey) ? primaryKey : 0;
                    }
                    else
                    {
                        string propertyName = property.Metadata.Name;
                        auditEntry.ObjectValues[propertyName] = property.OriginalValue;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLog.Add(auditEntry.ToAudit());
            }
        }

        public DbSet<Drone> Drone { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<AuditLog> AuditLog { get; set; }
    }
}
