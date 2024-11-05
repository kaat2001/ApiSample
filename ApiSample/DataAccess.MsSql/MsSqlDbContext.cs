using DataModel;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.MsSql;

public class MsSqlDbContext : DbContext, IDbContext
{
    public MsSqlDbContext()
    {
    }

    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options)
        : base(options)
    {
    }

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
