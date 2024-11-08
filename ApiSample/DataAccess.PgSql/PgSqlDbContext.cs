using DataModel.Common;
using DataModel.DbContexts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.PgSql;

public partial class PgSqlDbContext : AuditableDbContext, IDbContext
{
    public const string SchemaName = "public";


    public PgSqlDbContext(DbContextOptions<PgSqlDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            ConfigureAudit(modelBuilder, entityType);
        }
        modelBuilder.HasDefaultSchema(SchemaName);

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
        builder.Property(created.Name).HasDefaultValueSql("now()");
        builder.Property(lastModified.Name).HasDefaultValueSql("now()");
    }   
}
