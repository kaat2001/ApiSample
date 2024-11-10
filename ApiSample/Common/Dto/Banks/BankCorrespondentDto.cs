namespace Common.Interfaces.Dto.Banks;

public class BankCorrespondentDto :BankInfoDto
{
    public required bool IsDefault { get; set; } = false;
}
