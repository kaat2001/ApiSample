namespace Common.Dto.Employees;

public class EmployeeShortInfoDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public required DateTime BirthDate { get; set; }

}
