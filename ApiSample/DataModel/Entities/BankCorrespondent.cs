using Microsoft.EntityFrameworkCore;
using System;
namespace DataModel.Entities;


[PrimaryKey(nameof(BankInfoId), nameof(SupplierId))]
public class BankCorrespondent
{
    public required Guid BankInfoId { get; set; }

    public required BankInfo BankInfo { get; set; }

    public required Guid SupplierId { get; set; }

    public required Supplier Supplier { get; set; }

    public bool IsDefault { get; set; }
}
