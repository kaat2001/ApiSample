
using Microsoft.EntityFrameworkCore;

namespace DataModel.Entities;


[PrimaryKey(nameof(EmployeeId), nameof(RoleId))]
public class EmployeeRole
{
    public required Guid EmployeeId { get; set; }

    public required Employee Employee { get; set; }

    public required int RoleId { get; set; }

    public required Role Role { get; set; }
}