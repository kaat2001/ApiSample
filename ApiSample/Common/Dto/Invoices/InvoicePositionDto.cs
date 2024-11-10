using System.ComponentModel.DataAnnotations;

namespace Common.Interfaces.Dto.Invoices;

public class InvoicePositionDto
{
    public Guid? Id { get; set; }

    public int? Order { get; set; }

    public required string Description { get; set; }

    public decimal PositionAmount { get; set; }

    public decimal PayablePercent { get; set; }

    public decimal PayableAmount { get; set; }
}
