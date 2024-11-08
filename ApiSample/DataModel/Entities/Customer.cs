using DataModel.Common;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Entities;

public class Customer : AuditableEntity, IDeletable, IIdentity<Guid>
{
    public const int RegularLength = Limits.RegularString;

    public Guid Id { get; set; }
 
    [MaxLength(RegularLength)]
    public required string Name { get; set; }

    public bool IsDeleted { get; set; }
    
    public DateTimeOffset? Deleted { get; set; }

}
