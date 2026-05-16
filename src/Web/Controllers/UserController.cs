
using Application.DTOs.User.request;
using Application.DTOs.User.response;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    public ActionResult<GetByIdResponse> GetById(Guid Id)
    {
        var user = _userService.GetById(Id);
        return Ok(user);
    }

    [HttpGet]

    public async Task<ActionResult<List<GetAllResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAll(cancellationToken);
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest userDto, CancellationToken cancellationToken)
    {
        var user = await _userService.Create(userDto, cancellationToken);
        return Ok(user);
    }

    [HttpPut("{id}")]

    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest userDto, CancellationToken cancellationToken)
    {
        var user = await _userService.Update(id, userDto, cancellationToken);
        return Ok(user);
    }


    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _userService.Delete(id, cancellationToken);
        return NoContent();
    }
}