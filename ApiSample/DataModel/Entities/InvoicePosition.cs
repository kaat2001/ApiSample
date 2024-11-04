
using Common;
using DataModel.Common;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Entities;

public class InvoicePosition : AuditableEntity, IDeletable, IIdentity<Guid>
{
    public const int LongLength = Limits.LongString;

    public Guid Id { get; set; }
    
    public int? Order { get; set; }

    [MaxLength(LongLength)]
    public required string Description { get; set; }

    public decimal PositionAmount { get; set; }

    public decimal PayablePercent { get; set; }
    
    public decimal PayableAmount { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? Deleted { get; set; }
}
