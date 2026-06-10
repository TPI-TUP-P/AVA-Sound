
using System.Security.Claims;
using Application.DTOs.User.request;
using Application.DTOs.User.Request;
using Application.DTOs.User.response;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace Web.Controllers;

[Route("api/User")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]

    public ActionResult<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var user = _userService.GetById(Id, cancellationToken);
        return Ok(user);
    }


    [HttpGet]
    public async Task<ActionResult<PagerResponse<GetByIdResponse>>> GetAll([FromQuery] PagerRequest pagerRequest, CancellationToken cancellationToken)
{
    var result = await _userService.GetAll(pagerRequest, cancellationToken);

    return Ok(result);
}



    [HttpPost]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest userDto, CancellationToken cancellationToken)
    {
        var user = await _userService.Create(userDto, cancellationToken);
        return Ok(user);
    }

    [HttpPatch("{id}")]

    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest userDto, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ??User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
        {
            return Unauthorized("User ID not found in token.");
        }

        var idUser = Guid.Parse(idUserToken);

        var user = await _userService.Update(id, userDto, idUser, cancellationToken);
        return Ok(user);
    }


    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _userService.Delete(id, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id}/make-admin")]
    public async Task<ActionResult> MakeAdmin(Guid id, CancellationToken cancellationToken)
{
    
    Guid currentUserId = Guid.Parse(User.FindFirst("id")!.Value);

    await _userService.MakeAdmin(
        id,
        currentUserId,
        cancellationToken);

    return Ok("User updated as administrator");
}
}