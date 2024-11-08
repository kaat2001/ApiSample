using DataModel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataModel.DbContexts;

public abstract class AuditableDbContext : DbContext
{
    public AuditableDbContext(DbContextOptions options) : base(options)
    {
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetAuditProperties();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SetAuditProperties();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SetAuditProperties()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditableEntity auditable)
            {
                if (entry.State == EntityState.Added)
                {
                    auditable.Created = DateTimeOffset.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    auditable.LastModified = DateTimeOffset.UtcNow;
                }
            }

            if (entry is { Entity: IDeletable deletable })
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    deletable.IsDeleted = true;
                }

                if (deletable.IsDeleted && entry.Property(nameof(IDeletable.IsDeleted)).IsModified)
                {
                    deletable.Deleted = DateTimeOffset.UtcNow;
                }
            }

        }
    }

}
