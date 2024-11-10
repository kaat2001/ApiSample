using Common.Dto.Employees;
using Common.Interfaces.Repositories;
using DataModel.DbContexts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Common.Interfaces.Dto;

namespace Common.Implementations.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly IDbContext _db;
    private readonly IMapper _mapper;
    private readonly ILogger<SupplierRepository> _logger;

    public SupplierRepository(IDbContext db, IMapper mapper, ILogger<SupplierRepository> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> CreateNew(SupplierDto supplier, CancellationToken cancellationToken)
    {
        Guid supplierId = Guid.Empty;
        try
        {
            var newItem = _mapper.Map<Supplier>(supplier);
            newItem.Id = _db.NewId();
            
            await _db.Suppliers.AddAsync(newItem, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            supplierId = newItem.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during creating new supplier");
        }
        return supplierId;
    }

    public async Task<bool> Delete(Guid supplierId, CancellationToken cancellationToken)
    {
        var result = true;
        try
        {
            var item = await _db.Suppliers.FirstOrDefaultAsync(x => x.Id == supplierId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Supplier not found by Id={supplierId}");
            _db.Suppliers.Remove(item);

            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during deleting supplier");
            result = false;
        }
        return result;
    }

 
    public async Task<Guid> Update(SupplierDto supplier, CancellationToken cancellationToken)
    {
        try
        {
            if (!supplier.Id.HasValue)
                throw new ArgumentOutOfRangeException("Supplier Id cannot be null for Update method");

            var item = await _db.Suppliers.FirstOrDefaultAsync(x => x.Id == supplier.Id.Value, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Supplier not found by Id={supplier.Id}");

           item.Address = supplier.Address;
            //item.BankCorresponents = null;
            //item.BeneficiaryBank = null;
            item.Name = supplier.Name;

            await _db.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during updating new Supplier");
            return Guid.Empty;
        }
    }

    public async Task<List<SupplierDto>> GetAll(object filter, CancellationToken cancellationToken = default)
    {
        //TODO select not deleted records by default, without specifing it
        var items = await _db.Suppliers.Where(x => !x.IsDeleted).Select(item => _mapper.Map<SupplierDto>(item)).ToListAsync();
        return items;
    }

    public async Task<SupplierDto?> Get(Guid supplierId, CancellationToken cancellationToken = default)
    {
        SupplierDto? result = null;
        try
        {
            var item = await _db.Suppliers.FirstOrDefaultAsync(x => x.Id == supplierId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Supplier not found by Id={supplierId}");

            result = _mapper.Map<SupplierDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during getting specified supplier");
        }
        return result;
    }
}


