using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Transactions;

namespace DataModel.DbContexts
{
    public interface IDbContext
    {      
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();

        Guid NewId();

        DbSet<BankCorrespondent> BankCorrespondent { get; }
        DbSet<BankInfo> BankInfos { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Employee> Employees { get; }
        DbSet<EmployeeRole> EmployeeRoles { get; }
        DbSet<Invoice> Invoices { get; }
        DbSet<InvoicePosition> InvoicePositions { get; }
        DbSet<Role> Roles { get; }
        DbSet<Supplier> Suppliers { get; }

    }
}
