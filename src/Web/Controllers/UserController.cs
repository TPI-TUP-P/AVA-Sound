
using Application.DTOs.User.request;
using Application.DTOs.User.Request;
using Application.DTOs.User.response;
using Application.DTOs.User.Response;
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