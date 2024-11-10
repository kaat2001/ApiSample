using AutoMapper;
using Common.Interfaces.Dto;
using Common.Interfaces.Repositories;
using DataModel.DbContexts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Common.Implementations.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbContext _db;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerRepository> _logger;

    public CustomerRepository(IDbContext db, IMapper mapper, ILogger<CustomerRepository> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<Guid> CreateNew(CustomerDto customer, CancellationToken cancellationToken)
    {
        Guid customerId = Guid.Empty;
        try
        {
            var newItem = _mapper.Map<Customer>(customer);
            newItem.Id = _db.NewId();

            await _db.Customers.AddAsync(newItem, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            customerId = newItem.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during creating new customer");
        }
        return customerId;
    }

    public async Task<bool> Delete(Guid customerId, CancellationToken cancellationToken)
    {
        var result = true;
        try
        {
            var item = await _db.Customers.FirstOrDefaultAsync(x => x.Id == customerId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Customer not found by Id={customerId}");
            _db.Customers.Remove(item);

            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during deleting customer");
            result = false;
        }
        return result;
    }


    public async Task<Guid> Update(CustomerDto customer, CancellationToken cancellationToken)
    {
        try
        {
            if (!customer.Id.HasValue)
                throw new ArgumentOutOfRangeException("Customer Id cannot be null for Update method");

            var item = await _db.Customers.FirstOrDefaultAsync(x => x.Id == customer.Id.Value, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Supplier not found by Id={customer.Id}");

            item.Name = customer.Name;

            await _db.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during updating new Supplier");
            return Guid.Empty;
        }
    }

    public async Task<List<CustomerDto>> GetAll(object filter, CancellationToken cancellationToken = default)
    {
        //TODO select not deleted records by default, without specifing it
        var items = await _db.Customers.Where(x => !x.IsDeleted).Select(item => _mapper.Map<CustomerDto>(item)).ToListAsync();
        return items;
    }

    public async Task<CustomerDto?> Get(Guid customerId, CancellationToken cancellationToken = default)
    {
        CustomerDto? result = null;
        try
        {
            var item = await _db.Customers.FirstOrDefaultAsync(x => x.Id == customerId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Customer not found by Id={customerId}");

            result = _mapper.Map<CustomerDto>(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during getting specified customer");
        }
        return result;
    }
}


