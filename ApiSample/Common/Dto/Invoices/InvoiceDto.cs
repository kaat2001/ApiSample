
using System.ComponentModel.DataAnnotations;

namespace Common.Interfaces.Dto.Invoices;

public class InvoiceDto
{
    public Guid? Id { get; set; }

    public required string InvoiceNumber { get; set; }

    public required string ContractRef { get; set; }

    public DateTimeOffset? IssueDate { get; set; }

    public DateTimeOffset? DueDate { get; set; }

    public DateTimeOffset PeriodStartDate { get; set; }
    public DateTimeOffset PeriodEndDate { get; set; }

    public required CustomerDto Customer { get; set; }

    public required SupplierDto Supplier { get; set; }

    public List<InvoicePositionDto> Positions { get; set; } = new();

}
