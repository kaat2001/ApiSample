using Common.Dto.Employee;
using Common.Interfaces;

namespace ApiSample.Interfaces;

public class UserService : IUserService
{
    public Task<object> CreateEmployee(int v, object cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<object> DeleteEmployee(int v, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<object> UpdateEmployee(int v, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    Task<List<EmployeeShortInfoDto>> IUserService.GetListsAsync(object value, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
