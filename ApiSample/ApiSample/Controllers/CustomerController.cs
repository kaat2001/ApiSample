
using ApiSample.Queries;
using ApiSampleControllers.Extentions;
using Common.Dto.Employees;
using Common.Interfaces.Dto;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSample.Controllers;

//[Authorize]
[Route("api/v1/customer/[action]")]
[ApiController]

public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerService _customerService;
    private readonly IAuthorizationService _authorizationService;

    public CustomerController(ILogger<CustomerController> logger,
                              ICustomerService customerService,
                              IAuthorizationService authorizationService
                                   )
    {
        _logger = logger;
        _customerService = customerService; 
        _authorizationService = authorizationService;
    }

    [HttpGet]
    [Produces(typeof(List<CustomerDto>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CustomerDto>>> GetAll([FromQuery] BaseGetQuery query, CancellationToken cancellationToken)
    {
        var result = await _customerService.GetLists(User.GetId(), cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Produces(typeof(CustomerDto))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeInfoDto>> Get([FromQuery] Guid customerId, CancellationToken cancellationToken)
    {
        var result = await _customerService.GetById(User.GetId(), customerId, cancellationToken);
        if (result.IsSuccess)
        return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);

    }
    [HttpPost]
    [Produces(typeof(Guid))]

    public async Task<ActionResult<Guid>> Create(CustomerDto customerData, CancellationToken cancellationToken) 
    {
        var result = await _customerService.Create(User.GetId(), customerData, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpPatch]
    [Produces(typeof(Guid))]
    public async Task<ActionResult<Guid>> Update(CustomerDto customerData, CancellationToken cancellationToken)
    {
        var result = await _customerService.Update(User.GetId(), customerData, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpDelete]
    [Produces(typeof(bool))]
    public async Task<ActionResult<bool>> Delete(Guid customerId, CancellationToken cancellationToken)
    {
        var result = await _customerService.Delete(User.GetId(), customerId, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

}
