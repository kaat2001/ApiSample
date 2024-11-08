
using DataModel.Common;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Entities;

public class Employee :AuditableEntity, IDeletable, IIdentity<Guid>
{
    public const int RegularLength = Limits.RegularString;

    public Guid Id { get; set; }
    
    [MaxLength(RegularLength)]
    public required string FirstName { get; set; }
    
    [MaxLength(RegularLength)]
    public required string LastName { get; set; }
    
    [MaxLength(RegularLength)]
    public string? MiddleName { get; set; }
    
    [MaxLength(RegularLength)]
    public required string Email { get; set; }

    public required DateTime BirthDate { get; set; }
    
    public string? PasswordHash { get; set; }

    public Guid SecurityStamp { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public DateTimeOffset? Deleted { get; set; }

    public List<EmployeeRole> EmployeeRoles { get; init; } = new();

}
