using Common.Dto.Employee;
using Common.Interfaces.Repositories;
using DataModel;
using Microsoft.EntityFrameworkCore;

namespace ApiSample.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbContext _db;

    public EmployeeRepository(IDbContext db)
    {
        _db = db;
    }
    public async Task<List<EmployeeShortInfoDto>> GetAllAsync(object filter, CancellationToken cancellationToken = default)
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
}
