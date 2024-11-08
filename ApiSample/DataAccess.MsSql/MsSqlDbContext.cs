using DataModel.Common;
using DataModel.DbContexts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Transactions;

namespace DataAccess.MsSql;

public partial class MsSqlDbContext : AuditableDbContext, IDbContext
{
    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            ConfigureAudit(modelBuilder, entityType);
        }

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureAudit(ModelBuilder modelBuilder, IMutableEntityType entityType)
    {
        var created = entityType.FindDeclaredProperty(nameof(AuditableEntity.Created));
        var lastModified = entityType.FindDeclaredProperty(nameof(AuditableEntity.LastModified));
        if (created == null || lastModified == null)
        {
            return;
        }

        var builder = modelBuilder.Entity(entityType.ClrType);
        builder.Property(created.Name).HasDefaultValueSql("getdate()");
        builder.Property(lastModified.Name).HasDefaultValueSql("getdate()");
    }

}
