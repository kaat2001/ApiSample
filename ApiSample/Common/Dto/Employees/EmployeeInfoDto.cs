namespace Common.Dto.Employees;

public class EmployeeInfoDto
{
    public  Guid? Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime BirthDate { get; set; }
    public required string Email { get; set; }

}
