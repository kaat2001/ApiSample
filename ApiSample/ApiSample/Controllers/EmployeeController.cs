
using ApiSample.Queries;
using ApiSampleControllers.Extentions;
using Common.Dto.Employees;
using Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSample.Controllers;

//[Authorize]
[Route("api/v1/employee/[action]")]
[ApiController]

public class EmployeeController: ControllerBase
{
    private readonly ILogger<EmployeeController> _logger;
    private readonly IUserService _userService;
    private readonly IAuthorizationService _authorizationService;

    public EmployeeController(ILogger<EmployeeController> logger,
                                   IUserService userService,
                                   IAuthorizationService authorizationService
                                   )
    {
        _logger = logger;
        _userService = userService;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    [Produces(typeof(List<EmployeeShortInfoDto>))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<EmployeeShortInfoDto>>> GetAll([FromQuery] EmployeeGetQuery query, CancellationToken cancellationToken)
    {
        var result = await _userService.GetLists(User.GetId(), cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Produces(typeof(EmployeeInfoDto))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeInfoDto>> Get([FromQuery] Guid employeeId, CancellationToken cancellationToken)
    {
        var result = await _userService.GetById(User.GetId(), employeeId, cancellationToken);
        if (result.IsSuccess)
        return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);

    }
    [HttpPost]
    [Produces(typeof(Guid))]

    public async Task<ActionResult<Guid>> Create(EmployeeInfoDto employeeData, CancellationToken cancellationToken) 
    {
        var result = await _userService.Create(User.GetId(), employeeData, cancellationToken);

        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpPatch]
    [Produces(typeof(Guid))]
    public async Task<ActionResult<Guid>> Update(EmployeeInfoDto employeeData, CancellationToken cancellationToken)
    {
        var result = await _userService.Update(User.GetId(), employeeData, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

    [HttpDelete]
    [Produces(typeof(bool))]
    public async Task<ActionResult<bool>> Delete(Guid employeeId, CancellationToken cancellationToken)
    {
        var result = await _userService.Delete(User.GetId(), employeeId, cancellationToken);
        if (result.IsSuccess)
            return Ok(result.Value);
        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
    }

}
