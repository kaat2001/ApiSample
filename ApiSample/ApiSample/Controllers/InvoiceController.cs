
using ApiSample.Queries;
using ApiSampleControllers.Extentions;
using Common.Dto.Employees;
using Common.Interfaces.Dto.Invoices;
using Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSample.Controllers;

//[Authorize]
[Route("api/v1/invoice/[action]")]
[ApiController]

public class InvoiceController : ControllerBase
{
    private readonly ILogger<InvoiceController> _logger;
    private readonly IInvoiceService _invoiceService;
    private readonly IAuthorizationService _authorizationService;

    public InvoiceController(ILogger<InvoiceController> logger,
                                   IAuthorizationService authorizationService
                                   )
    {
        _logger = logger;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    [Produces(typeof(List<InvoiceDto>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<InvoiceDto>>> GetAll([FromQuery] BaseGetQuery query, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.GetList(User.GetId(), cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Produces(typeof(InvoiceDto))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeInfoDto>> Get([FromQuery] Guid invoiceId, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.GetInvoiceById(User.GetId(), invoiceId, cancellationToken);
        if (result.IsSuccess)
        return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);

    }
    [HttpPost]
    [Produces(typeof(Guid))]

    public async Task<ActionResult<Guid>> Create(InvoiceDto invoiceData, CancellationToken cancellationToken) 
    {
        var result = await _invoiceService.CreateInvoice(User.GetId(), invoiceData, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpPatch]
    [Produces(typeof(Guid))]
    public async Task<ActionResult<Guid>> Update(InvoiceDto invoiceData, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.UpdateInvoice(User.GetId(), invoiceData, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpDelete]
    [Produces(typeof(bool))]
    public async Task<ActionResult<bool>> Delete(Guid invoiceId, CancellationToken cancellationToken)
    {
        var result = await _invoiceService.DeleteInvoice(User.GetId(), invoiceId, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

}
