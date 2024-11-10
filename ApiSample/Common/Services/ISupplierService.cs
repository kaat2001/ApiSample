using Common.Interfaces.Dto;

namespace Common.Interfaces.Services;

public interface ISupplierService
{
    Task<BaseResponse<Guid>> Create(Guid userId, SupplierDto supplier, CancellationToken cancellationToken);
    Task<BaseResponse<Guid>> Update(Guid userId, SupplierDto supplier, CancellationToken cancellationToken);
    Task<BaseResponse<bool>> Delete(Guid userId, Guid supplierId, CancellationToken cancellationToken);
    Task<List<SupplierDto>> GetLists(object filters, CancellationToken cancellationToken);
    Task<BaseResponse<SupplierDto?>> GetById(Guid userId, Guid supplier, CancellationToken cancellationToken);
}
