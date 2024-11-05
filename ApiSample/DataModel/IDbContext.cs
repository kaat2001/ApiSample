using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IDbContext
    {
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
