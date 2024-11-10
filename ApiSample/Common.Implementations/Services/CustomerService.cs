using Common.Dto.Employees;
using Common.Interfaces.Dto;
using Common.Interfaces.Repositories;
using Common.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Common.Implementation.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ICustomerRepository customerRepository,
         ILogger<CustomerService> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
    }
    public async Task<BaseResponse<Guid>> Create(Guid userId, CustomerDto customer, CancellationToken cancellationToken)
    {
        var employeeId = await _customerRepository.CreateNew(customer, cancellationToken);

        var result = new BaseResponse<Guid>
        {
            IsSuccess = employeeId != Guid.Empty,
            ErrorMessage = employeeId != Guid.Empty ? string.Empty : "Error occuired",
            Value = employeeId
        };
        return result;
    }

    public async Task<BaseResponse<Guid>> Update(Guid userId, CustomerDto customer, CancellationToken cancellationToken)
    {
        var customerId = await _customerRepository.Update(customer, cancellationToken);
        var result = new BaseResponse<Guid>
        {
            IsSuccess = customerId != Guid.Empty,
            ErrorMessage = customerId != Guid.Empty ? string.Empty : "Error occuired",
            Value = customerId
        };
        return result;
    }

    public async Task<BaseResponse<bool>> Delete(Guid userId, Guid customerId, CancellationToken cancellationToken)
    {
        var value = await _customerRepository.Delete(customerId, cancellationToken);
        return new BaseResponse<bool>(value);
    }

    public async Task<List<CustomerDto>> GetLists(object filters, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetAll(filters, cancellationToken);
    }

    public async Task<BaseResponse<CustomerDto?>> GetById(Guid userId, Guid customerId, CancellationToken cancellationToken)
    {
        var customer =  await _customerRepository.Get(customerId, cancellationToken);
        if (customer!=null)
            return new BaseResponse<CustomerDto?>(customer);
        else return new BaseResponse<CustomerDto?>(false, "Error occiured", null);
    }
}
