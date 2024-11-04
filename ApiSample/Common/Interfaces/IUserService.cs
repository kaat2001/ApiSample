using Common.Dto.Employee;

namespace Common.Interfaces;

public interface IUserService
{
    Task<object> CreateEmployee(int v, object cancellationToken);
    Task<object> UpdateEmployee(int v, CancellationToken cancellationToken);
    Task<object> DeleteEmployee(int v, CancellationToken cancellationToken);
    Task<List<EmployeeShortInfoDto>> GetListsAsync(object value, CancellationToken cancellationToken);
}
