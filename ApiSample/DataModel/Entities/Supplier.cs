using DataModel.Common;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Entities;

public class Supplier : AuditableEntity, IDeletable, IIdentity<Guid>
{
    public const int RegularLength = Limits.RegularString;
    public const int LongLength = Limits.LongString;

    public Guid Id { get; set; }

    [MaxLength(RegularLength)]
    public required string Name { get; set; }
    
    [MaxLength(LongLength)]
    public required string Address { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? Deleted { get; set; }

    public required Guid BeneficiaryBankId { get; set; }

    public required BankInfo BeneficiaryBank { get; set; }

    public List<BankCorrespondent> BankCorresponents { get; init; } = new();


}