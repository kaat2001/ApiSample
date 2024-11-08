using DataModel.Common;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Entities;

public class Invoice : AuditableEntity, IDeletable, IIdentity<Guid>
{
    public const int ShortLength = Limits.ShortString;

    public Guid Id { get; set; }

    [MaxLength(ShortLength)]
    public required string InvoiceNumber { get; set; }

    [MaxLength(ShortLength)]
    public required string ContractRef { get; set; }

    public DateTimeOffset? IssueDate { get; set; }
    
    public DateTimeOffset? DueDate { get; set; }

    public DateTimeOffset PeriodStartDate { get; set; }
    public DateTimeOffset PeriodEndDate { get; set; }

    public Guid CustomerId { get; set; }
    public required Customer Customer { get; set; }

    public Guid SupplierId { get; set; }
    public required Supplier Supplier { get; set; }

    public List<InvoicePosition> Positions { get; set; } = new();

    public bool IsDeleted { get; set; }

    public DateTimeOffset? Deleted { get; set; }

}
