using Common.Interfaces.Dto;

namespace Common.Interfaces.Services;

public interface ICustomerService
{
    Task<BaseResponse<Guid>> Create(Guid userId, CustomerDto customer, CancellationToken cancellationToken);
    Task<BaseResponse<Guid>> Update(Guid userId, CustomerDto customer, CancellationToken cancellationToken);
    Task<BaseResponse<bool>> Delete(Guid userId, Guid customerId, CancellationToken cancellationToken);
    Task<List<CustomerDto>> GetLists(object filters, CancellationToken cancellationToken);
    Task<BaseResponse<CustomerDto?>> GetById(Guid userId, Guid customerId, CancellationToken cancellationToken);
}
