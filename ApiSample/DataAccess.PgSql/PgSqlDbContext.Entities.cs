using DataModel.Common;
using DataModel.DbContexts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.PgSql;

public partial class PgSqlDbContext : AuditableDbContext, IDbContext
{
    public Guid NewId() => Guid.NewGuid();

    public DbSet<BankCorrespondent> BankCorrespondent { get; init; }
    public DbSet<BankInfo> BankInfos { get; init; }
    public DbSet<Customer> Customers { get; init; }
    public DbSet<Employee> Employees { get; init; }
    public DbSet<EmployeeRole> EmployeeRoles { get; init; }
    public DbSet<Invoice> Invoices { get; init; }
    public DbSet<InvoicePosition> InvoicePositions { get; init; }
    public DbSet<Role> Roles { get; init; }
    public DbSet<Supplier> Suppliers { get; init; }
}
