using Microsoft.EntityFrameworkCore;
namespace DataModel.Entities;


[PrimaryKey(nameof(BankInfoId), nameof(SupplierId))]
public class BankCorrespondent
{
    public required Guid BankInfoId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public required BankInfo BankInfo { get; set; }

    public required Guid SupplierId { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public required Supplier Supplier { get; set; }

    public bool IsDefault { get; set; }
}
