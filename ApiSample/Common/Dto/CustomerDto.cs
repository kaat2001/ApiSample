namespace Common.Interfaces.Dto;

public class CustomerDto
{
    public Guid? Id { get; set; }

    public required string Name { get; set; }
}
