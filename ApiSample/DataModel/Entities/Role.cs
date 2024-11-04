using Common;
using DataModel.Common;
using System.ComponentModel.DataAnnotations;

namespace DataModel.Entities;

public class Role : IDeletable, IIdentity<int>
{
    public const int MaxNameLength = Limits.RegularString;

    public int Id { get; init; }

    [MaxLength(MaxNameLength)]
    public required string Name { get; set; }

    public List<EmployeeRole> EmployeeRoles { get; init; } = new();

    public bool IsDeleted { get; set; }

    public DateTimeOffset? Deleted { get; set; }
}
