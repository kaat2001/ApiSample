
using DataModel.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Entities;

public class InvoicePosition : AuditableEntity, IDeletable, IIdentity<Guid>
{
    public const int LongLength = Limits.LongString;

    public Guid Id { get; set; }
    
    public int? Order { get; set; }

    [MaxLength(LongLength)]
    public required string Description { get; set; }

    [Precision(16, 4)]
    public decimal PositionAmount { get; set; }

    [Precision(8, 2)]
    public decimal PayablePercent { get; set; }
    
    [Precision(16, 4)]
    public decimal PayableAmount { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? Deleted { get; set; }
}
