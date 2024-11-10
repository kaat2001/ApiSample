
using ApiSample.Queries;
using ApiSampleControllers.Extentions;
using Common.Dto.Employees;
using Common.Interfaces.Dto;
using Common.Interfaces.Dto.Invoices;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSample.Controllers;

//[Authorize]
[Route("api/v1/employee/[action]")]
[ApiController]

public class SupplierController : ControllerBase
{
    private readonly ILogger<SupplierController> _logger;
    private readonly ISupplierService _supplierService;
    private readonly IAuthorizationService _authorizationService;

    public SupplierController(ILogger<SupplierController> logger,
                                   IAuthorizationService authorizationService
                                   )
    {
        _logger = logger;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    [Produces(typeof(List<SupplierDto>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<InvoiceDto>>> GetAll([FromQuery] BaseGetQuery query, CancellationToken cancellationToken)
    {
        var result = await _supplierService.GetLists(User.GetId(), cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Produces(typeof(SupplierDto))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SupplierDto>> Get([FromQuery] Guid supplier, CancellationToken cancellationToken)
    {
        var result = await _supplierService.GetById(User.GetId(), supplier, cancellationToken);
        if (result.IsSuccess)
        return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);

    }
    [HttpPost]
    [Produces(typeof(Guid))]

    public async Task<ActionResult<Guid>> Create(SupplierDto supplierData, CancellationToken cancellationToken) 
    {
        var result = await _supplierService.Create(User.GetId(), supplierData, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpPatch]
    [Produces(typeof(Guid))]
    public async Task<ActionResult<Guid>> Update(SupplierDto supplierData, CancellationToken cancellationToken)
    {
        var result = await _supplierService.Update(User.GetId(), supplierData, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpDelete]
    [Produces(typeof(bool))]
    public async Task<ActionResult<bool>> Delete(Guid supplierId, CancellationToken cancellationToken)
    {
        var result = await _supplierService.Delete(User.GetId(), supplierId, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

}
