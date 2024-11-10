using Common.Interfaces.Dto.Banks;

namespace Common.Interfaces.Dto;

public class SupplierDto
{
    public Guid? Id { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }


    public required BankInfoDto BeneficiaryBank { get; set; }

    public List<BankCorrespondentDto> BankCorresponents { get; init; } = new();
}
