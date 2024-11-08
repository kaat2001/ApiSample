using Common.Dto.Employees;

namespace Common.Interfaces;

public interface IUserService
{
    Task<BaseResponse<Guid>> Create(Guid userId, EmployeeInfoDto employee, CancellationToken cancellationToken);
    Task<BaseResponse<Guid>> Update(Guid userId, EmployeeInfoDto employee, CancellationToken cancellationToken);
    Task<BaseResponse<bool>> Delete(Guid userId, Guid employeeId, CancellationToken cancellationToken);
    Task<List<EmployeeShortInfoDto>> GetLists(object filters, CancellationToken cancellationToken);
    Task<BaseResponse<EmployeeShortInfoDto?>> GetById(Guid userId, Guid employeeId, CancellationToken cancellationToken);

}
