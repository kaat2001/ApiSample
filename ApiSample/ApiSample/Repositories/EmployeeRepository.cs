using Common.Dto.Employees;
using Common.Interfaces.Repositories;
using DataModel.DbContexts;
using DataModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiSample.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbContext _db;
    private readonly ILogger<EmployeeRepository> _logger;

    public EmployeeRepository(IDbContext db, ILogger<EmployeeRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Guid> CreateNew(EmployeeInfoDto employee, CancellationToken cancellationToken)
    {
        Guid employeeId = Guid.Empty;
        try
        {
            var newItem = new Employee
            {
                BirthDate = employee.BirthDate,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            };
            newItem.Id = _db.NewId();
            await _db.Employees.AddAsync(newItem, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            employeeId = newItem.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during creating new employee");
        }
        return employeeId;
    }

    public async Task<bool> Delete(Guid employeeId, CancellationToken cancellationToken)
    {
        var result = true;
        try
        {
            var item = await _db.Employees.FirstOrDefaultAsync(x => x.Id == employeeId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Employee not found by Id={employeeId}");
            _db.Employees.Remove(item);

            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during deleting employee");
            result = false;
        }
        return result;
    }

 
    public async Task<Guid> Update(EmployeeInfoDto employee, CancellationToken cancellationToken)
    {
        try
        {
            if (!employee.Id.HasValue)
                throw new ArgumentOutOfRangeException("Employee Id cannot be null for Update method");

            var item = await _db.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id.Value, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Employee not found by Id={employee.Id}");

            item.BirthDate = employee.BirthDate;
            item.Email = employee.Email;
            item.FirstName = employee.FirstName;
            item.LastName = employee.LastName;

            await _db.SaveChangesAsync(cancellationToken);
            return item.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during creating new employee");
            return Guid.Empty;
        }
    }

    public async Task<List<EmployeeShortInfoDto>> GetAll(object filter, CancellationToken cancellationToken = default)
    {
        //TODO mapper
        //TODO select not deleted records by default, without specifing it
        var items = await _db.Employees.Where(x => !x.IsDeleted).ToListAsync();
        return items.Select(x => new EmployeeShortInfoDto
        {
            Id = x.Id,
            BirthDate = x.BirthDate,
            FirstName = x.FirstName,
            LastName = x.LastName
        }).ToList();
    }

    public async Task<EmployeeShortInfoDto?> Get(Guid employeeId, CancellationToken cancellationToken = default)
    {
        EmployeeShortInfoDto? result = null;
        try
        {
            var item = await _db.Employees.FirstOrDefaultAsync(x => x.Id == employeeId, cancellationToken);
            if (item == null)
                throw new ArgumentOutOfRangeException($"Employee not found by Id={employeeId}");

            result = new EmployeeShortInfoDto()
            {
                BirthDate = item.BirthDate,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Id = item.Id
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occuired during creating new employee");
        }
        return result;
    }
}


