using AutoMapper;
using Common.Interfaces.Dto.Invoices;
using Common.Interfaces.Repositories;
using DataModel.DbContexts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Implementations.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly IDbContext _db;
    private readonly IMapper _mapper;
    private readonly ILogger<InvoiceRepository> _logger;

    public InvoiceRepository(IDbContext db, IMapper mapper, ILogger<InvoiceRepository> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> CreateNewInvoice(InvoiceDto invoice, CancellationToken cancellationToken)
    {
        Guid invoiceId = Guid.Empty;
        try
        {
            var newItem = _mapper.Map<Invoice>(invoice);

            newItem.Id = _db.NewId();
            await _db.Invoices.AddAsync(newItem, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            invoiceId = newItem.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during creating new invoice");
        }
        return invoiceId;
    }

    public async Task<Guid> CreateNewInvoicePosition(InvoicePositionDto invoicePosition, CancellationToken cancellationToken)
    {
        Guid invoiceId = Guid.Empty;
        try
        {
            var newItem = _mapper.Map<InvoicePosition>(invoicePosition);

            newItem.Id = _db.NewId();
            await _db.InvoicePositions.AddAsync(newItem, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            invoiceId = newItem.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during creating new invoice position");
        }
        return invoiceId;
    }
    public async Task<bool> DeleteInvoice(Guid invoiceId, CancellationToken cancellationToken)
    {
        var result = true;
        try
        {
            var item = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Invoice not found by Id={invoiceId}");
            _db.Invoices.Remove(item);

            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during deleting invoice");
            result = false;
        }
        return result;
    }
    public async Task<bool> DeleteInvoicePosition(Guid invoicePositionId, CancellationToken cancellationToken)
    {
        var result = true;
        try
        {
            var item = await _db.InvoicePositions.FirstOrDefaultAsync(x => x.Id == invoicePositionId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Invoice Position not found by Id={invoicePositionId}");
            _db.InvoicePositions.Remove(item);

            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during deleting invoice position");
            result = false;
        }
        return result;
    }
    public async Task<Guid> UpdateInvoice(InvoiceDto invoice, CancellationToken cancellationToken)
    {
        try
        {
            if (!invoice.Id.HasValue)
                throw new ArgumentOutOfRangeException("Invoice Id cannot be null for Update method");

            var item = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == invoice.Id.Value, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Invoice not found by Id={invoice.Id}");

            item.IssueDate = invoice.IssueDate;
            item.InvoiceNumber = invoice.InvoiceNumber;
            item.ContractRef = invoice.ContractRef;
            item.PeriodEndDate = invoice.PeriodEndDate;
            item.PeriodStartDate = invoice.PeriodStartDate;

            //item.Positions
            //item.Supplier
            //item.customer
            item.DueDate = invoice.DueDate;

            await _db.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during invoice update ");
            return Guid.Empty;
        }
    }

    public async Task<Guid> UpdateInvoicePosition(InvoicePositionDto invoicePosition, CancellationToken cancellationToken) {
        try
        {
            if (!invoicePosition.Id.HasValue)
                throw new ArgumentOutOfRangeException("Invoice Position Id cannot be null for Update method");

            var item = await _db.InvoicePositions.FirstOrDefaultAsync(x => x.Id == invoicePosition.Id.Value, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Invoice Position not found by Id={invoicePosition.Id}");

            item.Order = invoicePosition.Order;
            item.PayableAmount = invoicePosition.PayableAmount;
            item.PayablePercent = invoicePosition.PayablePercent;
            item.PositionAmount = invoicePosition.PositionAmount;
            item.Description = invoicePosition.Description;

            await _db.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during invoice update ");
            return Guid.Empty;
        }
    }
    
    public async Task<List<InvoiceDto>> GetAll(object filter, CancellationToken cancellationToken = default) {
        var items = await _db.Invoices.Where(x => !x.IsDeleted).Select(item => _mapper.Map<InvoiceDto>(item)).ToListAsync();
        return items;
    }
    public async Task<InvoiceDto?> Get(Guid invoiceId, CancellationToken cancellationToken = default) {
        InvoiceDto? result = null;
        try
        {
            var item = await _db.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Invoice not found by Id={invoiceId}");

            result = _mapper.Map<InvoiceDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during getting specified invoice");
        }
        return result;
    }
    public async Task<List<InvoicePositionDto>> GetInvoicePositions(Guid invoiceId, CancellationToken cancellationToken = default) {
        var items = await _db.InvoicePositions.Where(x => x.InvoiceId == invoiceId).Select(item => _mapper.Map<InvoicePositionDto>(item)).ToListAsync();
        return items;
    }
    public async Task<InvoicePositionDto?> GetInvoicePosition(Guid invoicePositionId, CancellationToken cancellationToken = default) {
        InvoicePositionDto? result = null;
        try
        {
            var item = await _db.InvoicePositions.FirstOrDefaultAsync(x => x.Id == invoicePositionId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Invoice position not found by Id={invoicePositionId}");

            result = _mapper.Map<InvoicePositionDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during getting specified invoice position");
        }
        return result;
    }

}
