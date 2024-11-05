using ApiSample.Controllers;
using Common.Dto.Employee;
using Common.Interfaces;
using Common.Interfaces.Repositories;

namespace ApiSample.Interfaces;

public class UserService : IUserService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IEmployeeRepository employeeRepository,
         ILogger<UserService> logger)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
}
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

    Task<List<EmployeeShortInfoDto>> IUserService.GetListsAsync(object filters, CancellationToken cancellationToken)
    {
        //TODO convert filters object to query params
        return _employeeRepository.GetAllAsync(filters, cancellationToken);
    }
}
