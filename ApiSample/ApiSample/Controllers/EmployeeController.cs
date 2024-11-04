
using ApiSample.Mutations;
using ApiSample.Queries;
using ApiSample.Results;
using ApiSampleControllers.Extentions;
using Common.Dto.Employee;
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
                                   IAuthorizationService authorizationService)
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
        var result = await _userService.GetListsAsync(User.GetId(), cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Produces(typeof(EmployeeInfoDto))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<EmployeeInfoDto>> Get([FromQuery] Guid guid, CancellationToken cancellationToken)
    {
        //var result = await _userService.GetByIdAsync(User.GetId(), cancellationToken);

        return Ok();
    }
    [HttpPost]
    [Produces(typeof(ModificationResult))]

    public async Task<ActionResult<ModificationResult>> Create(SaveUserMutation command, CancellationToken cancellationToken) 
    {
        var user = await _userService.CreateEmployee(User.GetId(), cancellationToken);

        return Ok(new ModificationResult());
    }

    [HttpPatch]
    [Produces(typeof(ModificationResult))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModificationResult>> Update(SaveUserMutation command, CancellationToken cancellationToken)
    {
        var user = await _userService.UpdateEmployee(User.GetId(), cancellationToken);
        return Ok(new ModificationResult());
    }

    [HttpDelete]
    [Produces(typeof(ModificationResult))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModificationResult>> Delete(SaveUserMutation command, CancellationToken cancellationToken)
    {
        var user = await _userService.DeleteEmployee(User.GetId(), cancellationToken);
        return Ok(new ModificationResult());
    }

}
