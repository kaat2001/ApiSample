namespace Common.Interfaces.Dto.Banks;

public class BankInfoDto
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Address { get; set; }

    public required string Swift { get; set; }

    public required string Iban { get; set; }
}
