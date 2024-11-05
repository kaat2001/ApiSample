using Common.Dto.Employee;

namespace Common.Interfaces.Repositories;

public interface  IEmployeeRepository
{
    Task<List<EmployeeShortInfoDto>> GetAllAsync(object filter, CancellationToken cancellationToken = default);
}
