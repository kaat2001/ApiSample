using Common;
using Common.Dto.Employees;
using Common.Interfaces;
using Common.Interfaces.Repositories;
using DataModel.Entities;

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
    public async Task<BaseResponse<Guid>> Create(Guid userId, EmployeeInfoDto employee, CancellationToken cancellationToken)
    {
        var employeeId = await _employeeRepository.CreateNew(employee, cancellationToken);

        var result = new BaseResponse<Guid>
        {
            IsSuccess = employeeId != Guid.Empty,
            ErrorMessage = employeeId != Guid.Empty ? string.Empty : "Error occuired",
            Value = employeeId
        };
        return result;
    }

    public async Task<BaseResponse<Guid>> Update(Guid userId, EmployeeInfoDto employee, CancellationToken cancellationToken)
    {
        var employeeId = await _employeeRepository.Update(employee, cancellationToken);
        var result = new BaseResponse<Guid>
        {
            IsSuccess = employeeId != Guid.Empty,
            ErrorMessage = employeeId != Guid.Empty ? string.Empty : "Error occuired",
            Value = employeeId
        };
        return result;
    }

    public async Task<BaseResponse<bool>> Delete(Guid userId, Guid employeeId, CancellationToken cancellationToken)
    {
        var value = await _employeeRepository.Delete(employeeId, cancellationToken);
        return new BaseResponse<bool>(value);
    }

    public async Task<List<EmployeeShortInfoDto>> GetLists(object filters, CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetAll(filters, cancellationToken);
    }

    public async Task<BaseResponse<EmployeeShortInfoDto?>> GetById(Guid userId, Guid employeeId, CancellationToken cancellationToken)
    {
        var employee =  await _employeeRepository.Get(employeeId, cancellationToken);
        if (employee!=null)
            return new BaseResponse<EmployeeShortInfoDto>(employee);
        else return new BaseResponse<EmployeeShortInfoDto?>(false, "Error occiured", null);
    }
}
