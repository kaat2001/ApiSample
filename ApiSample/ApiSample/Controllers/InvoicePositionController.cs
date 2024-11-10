
using ApiSample.Queries;
using ApiSampleControllers.Extentions;
using Common.Dto.Employees;
using Common.Interfaces.Dto.Invoices;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSample.Controllers;

//[Authorize]
[Route("api/v1/employee/[action]")]
[ApiController]

public class InvoicePositionController : ControllerBase
{
    private readonly ILogger<InvoicePositionController> _logger;
    private readonly IInvoiceService _invoiceService;
    private readonly IAuthorizationService _authorizationService;

    public InvoicePositionController(ILogger<InvoicePositionController> logger,
                                   IAuthorizationService authorizationService
                                   )
    {
        _logger = logger;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    [Produces(typeof(List<InvoicePositionDto>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<InvoiceDto>>> GetAll([FromQuery] Guid invoiceId, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.GetInvoicePositionList(User.GetId(), invoiceId, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Produces(typeof(InvoicePositionDto))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeInfoDto>> Get([FromQuery] Guid invoicePositionId, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.GetInvoicePossitionById(User.GetId(), invoicePositionId, cancellationToken);
        if (result.IsSuccess)
        return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);

    }
    [HttpPost]
    [Produces(typeof(Guid))]

    public async Task<ActionResult<Guid>> Create(InvoicePositionDto invoicePositionData, CancellationToken cancellationToken) 
    {
        var result = await _invoiceService.CreateInvoicePosition(User.GetId(), invoicePositionData, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpPatch]
    [Produces(typeof(Guid))]
    public async Task<ActionResult<Guid>> Update(InvoicePositionDto invoicePositionData, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.UpdateInvoicePosition(User.GetId(), invoicePositionData, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpDelete]
    [Produces(typeof(bool))]
    public async Task<ActionResult<bool>> Delete(Guid invoicePositionId, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.DeleteInvoicePosition(User.GetId(), invoicePositionId, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

}
