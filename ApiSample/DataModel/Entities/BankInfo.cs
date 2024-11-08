using DataModel.Common;
using System.ComponentModel.DataAnnotations;
namespace DataModel.Entities
{
    public class BankInfo : AuditableEntity, IDeletable, IIdentity<Guid>
    {
        public const int ShortestLength = Limits.ShortestString;
        public const int ShortLength = Limits.ShortString;
        public const int RegularLength = Limits.RegularString;
        public const int LongLength = Limits.LongString;

        public Guid Id { get; set; }

        [MaxLength(RegularLength)]
        public required string Name { get; set; }
        
        [MaxLength(LongLength)]
        public string? Address { get; set; }

        [MaxLength(ShortestLength)]
        public required string Swift { get; set; }
        
        [MaxLength(ShortLength)]
        public required string Iban { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset? Deleted { get; set; }
    }
}
