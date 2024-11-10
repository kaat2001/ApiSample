using Common.Interfaces.Dto.Invoices;

namespace Common.Interfaces.Repositories;

public interface  IInvoiceRepository
{
    Task<Guid> CreateNewInvoice(InvoiceDto invoice, CancellationToken cancellationToken);
    Task<Guid> CreateNewInvoicePosition(InvoicePositionDto invoicePosition, CancellationToken cancellationToken);
    Task<bool> DeleteInvoice(Guid invoiceId, CancellationToken cancellationToken);
    Task<bool> DeleteInvoicePosition(Guid invoicePositionId, CancellationToken cancellationToken);
    Task<Guid> UpdateInvoice(InvoiceDto employee, CancellationToken cancellationToken);
    Task<Guid> UpdateInvoicePosition(InvoicePositionDto employee, CancellationToken cancellationToken);
    Task<List<InvoiceDto>> GetAll(object filter, CancellationToken cancellationToken = default);
    Task<InvoiceDto?> Get(Guid invoiceId, CancellationToken cancellationToken = default);
    Task<List<InvoicePositionDto>> GetInvoicePositions(Guid invoiceId, CancellationToken cancellationToken = default);
    Task<InvoicePositionDto?> GetInvoicePosition(Guid invoicePositionId, CancellationToken cancellationToken = default);

}
