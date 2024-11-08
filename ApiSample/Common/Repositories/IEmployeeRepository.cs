using Common.Dto.Employees;

namespace Common.Interfaces.Repositories;

public interface  IEmployeeRepository
{
    Task<Guid> CreateNew(EmployeeInfoDto employee, CancellationToken cancellationToken);
    Task<bool> Delete(Guid employeeId, CancellationToken cancellationToken);
    Task<Guid> Update(EmployeeInfoDto employee, CancellationToken cancellationToken);
    Task<List<EmployeeShortInfoDto>> GetAll(object filter, CancellationToken cancellationToken = default);
    Task<EmployeeShortInfoDto?> Get(Guid employeeId, CancellationToken cancellationToken = default);

}
