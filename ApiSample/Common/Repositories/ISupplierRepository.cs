
using Common.Interfaces.Dto;

namespace Common.Interfaces.Repositories;

public interface  ISupplierRepository
{
    Task<Guid> CreateNew(SupplierDto supplier, CancellationToken cancellationToken);
    Task<bool> Delete(Guid supplierId, CancellationToken cancellationToken);
    Task<Guid> Update(SupplierDto supplier, CancellationToken cancellationToken);
    Task<List<SupplierDto>> GetAll(object filter, CancellationToken cancellationToken = default);
    Task<SupplierDto?> Get(Guid supplierId, CancellationToken cancellationToken = default);

}
