using Common.Dto.Employees;
using Common.Interfaces.Dto.Invoices;

namespace Common.Interfaces.Services;

public interface IInvoiceService
{
    Task<BaseResponse<Guid>> CreateInvoice(Guid userId, InvoiceDto invoice, CancellationToken cancellationToken);
    Task<BaseResponse<Guid>> UpdateInvoice(Guid userId, InvoiceDto invoice, CancellationToken cancellationToken);
    Task<BaseResponse<bool>> DeleteInvoice(Guid userId, Guid invoiceId, CancellationToken cancellationToken);
    Task<List<InvoiceDto>> GetList(object filters, CancellationToken cancellationToken);
    Task<BaseResponse<InvoiceDto?>> GetInvoiceById(Guid userId, Guid invoiceId, CancellationToken cancellationToken);

    Task<BaseResponse<Guid>> CreateInvoicePosition(Guid userId, InvoicePositionDto invoicePosition, CancellationToken cancellationToken);
    Task<BaseResponse<Guid>> UpdateInvoicePosition(Guid userId, InvoicePositionDto invoicePosition, CancellationToken cancellationToken);
    Task<BaseResponse<bool>> DeleteInvoicePosition(Guid userId, Guid invoicePositionId, CancellationToken cancellationToken);
    Task<List<InvoicePositionDto>> GetInvoicePositionList(Guid userId, Guid invoicePositionId, CancellationToken cancellationToken);
    Task<BaseResponse<InvoicePositionDto?>> GetInvoicePossitionById(Guid userId, Guid invoiceId, CancellationToken cancellationToken);

}
