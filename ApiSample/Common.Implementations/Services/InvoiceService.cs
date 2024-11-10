using Common.Dto.Employees;
using Common.Interfaces.Dto.Invoices;
using Common.Interfaces.Repositories;
using Common.Interfaces.Services;
using DataModel.Entities;
using Microsoft.Extensions.Logging;

namespace Common.Implementation.Services;

public class InvoiceService : IInvoiceService
{
    private readonly InvoiceRepository _invoiceRepository;
    private readonly ILogger<InvoiceService> _logger;

    public InvoiceService(InvoiceRepository invoiceRepository,
         ILogger<InvoiceService> logger)
    {
        _invoiceRepository = invoiceRepository;
        _logger = logger;
    } 

    public async Task<BaseResponse<Guid>> CreateInvoice(Guid userId, InvoiceDto invoice, CancellationToken cancellationToken)
    {
        var invoiceId = await _invoiceRepository.CreateNewInvoice(invoice, cancellationToken);

        var result = new BaseResponse<Guid>
        {
            IsSuccess = invoiceId != Guid.Empty,
            ErrorMessage = invoiceId != Guid.Empty ? string.Empty : "Error occuired",
            Value = invoiceId
        };
        return result;
    }

    public async Task<BaseResponse<Guid>> UpdateInvoice(Guid userId, InvoiceDto invoice, CancellationToken cancellationToken)
    {
        var invoiceId = await _invoiceRepository.UpdateInvoice(invoice, cancellationToken);
        var result = new BaseResponse<Guid>
        {
            IsSuccess = invoiceId != Guid.Empty,
            ErrorMessage = invoiceId != Guid.Empty ? string.Empty : "Error occuired",
            Value = invoiceId
        };
        return result;
    }

    public async Task<BaseResponse<bool>> DeleteInvoice(Guid userId, Guid invoiceId, CancellationToken cancellationToken)
    {
        var value = await _invoiceRepository.DeleteInvoice(invoiceId, cancellationToken);
        return new BaseResponse<bool>(value);
    }

    public async Task<List<InvoiceDto>> GetList(object filters, CancellationToken cancellationToken)
    {
        return await _invoiceRepository.GetAll(filters, cancellationToken);
    }

    public async Task<BaseResponse<InvoiceDto?>> GetInvoiceById(Guid userId, Guid invoiceId, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.Get(invoiceId, cancellationToken);
        if (invoice != null)
            return new BaseResponse<InvoiceDto?>(invoice);
        else return new BaseResponse<InvoiceDto?>(false, "Error occiured", null);
    }

    public async Task<BaseResponse<Guid>> CreateInvoicePosition(Guid userId, InvoicePositionDto invoicePosition, CancellationToken cancellationToken)
    {
        var invoicePositionId = await _invoiceRepository.CreateNewInvoicePosition(invoicePosition, cancellationToken);

        var result = new BaseResponse<Guid>
        {
            IsSuccess = invoicePositionId != Guid.Empty,
            ErrorMessage = invoicePositionId != Guid.Empty ? string.Empty : "Error occuired",
            Value = invoicePositionId
        };
        return result;
    }

    public async Task<BaseResponse<Guid>> UpdateInvoicePosition(Guid userId, InvoicePositionDto invoicePosition, CancellationToken cancellationToken)
    {
        var invoicePositionId = await _invoiceRepository.UpdateInvoicePosition(invoicePosition, cancellationToken);
        var result = new BaseResponse<Guid>
        {
            IsSuccess = invoicePositionId != Guid.Empty,
            ErrorMessage = invoicePositionId != Guid.Empty ? string.Empty : "Error occuired",
            Value = invoicePositionId
        };
        return result;
    }

    public async Task<BaseResponse<bool>> DeleteInvoicePosition(Guid userId, Guid invoicePositionId, CancellationToken cancellationToken)
    {
        var value = await _invoiceRepository.DeleteInvoicePosition(invoicePositionId, cancellationToken);
        return new BaseResponse<bool>(value);
    }

    public async Task<List<InvoicePositionDto>> GetInvoicePositionList(Guid userId, Guid invoiceId, CancellationToken cancellationToken)
    {
        return await _invoiceRepository.GetInvoicePositions(invoiceId, cancellationToken);

    }

    public async Task<BaseResponse<InvoicePositionDto?>> GetInvoicePossitionById(Guid userId, Guid invoicePositionId, CancellationToken cancellationToken)
    {
        var invoicePosition = await _invoiceRepository.GetInvoicePosition(invoicePositionId, cancellationToken);
        if (invoicePosition != null)
            return new BaseResponse<InvoicePositionDto?>(invoicePosition);
        else return new BaseResponse<InvoicePositionDto?>(false, "Error occiured", null);
    }
}
