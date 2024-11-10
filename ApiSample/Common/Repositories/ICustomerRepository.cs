
using Common.Interfaces.Dto;

namespace Common.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<Guid> CreateNew(CustomerDto customer, CancellationToken cancellationToken);
    Task<bool> Delete(Guid customerId, CancellationToken cancellationToken);
    Task<Guid> Update(CustomerDto customer, CancellationToken cancellationToken);
    Task<List<CustomerDto>> GetAll(object filter, CancellationToken cancellationToken = default);
    Task<CustomerDto?> Get(Guid customerId, CancellationToken cancellationToken = default);

}
