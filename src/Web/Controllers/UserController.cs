
using System.Security.Claims;
using Application.DTOs.User.request;
using Application.DTOs.User.Request;
using Application.DTOs.User.response;
using Application.DTOs.User.Response;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    
    [EnableRateLimiting("HardyEndpoint")]
    public async Task<ActionResult<GetByIdResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetById(id, cancellationToken);
        return Ok(user);
    }


    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin")]
    [EnableRateLimiting("HardyEndpoint")]
    public async Task<ActionResult<PagerResponse<GetByIdResponse>>> GetAll(CancellationToken cancellationToken)
{
    var result = await _userService.GetAll(cancellationToken);

    return Ok(result);
}


    [HttpGet("GetAllArtist")]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<GetAllArtistResponse>> GetAllArtists(CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllArtists(cancellationToken);
        return Ok(result);
    }


    [HttpPost]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest userDto, CancellationToken cancellationToken)
    {
        var user = await _userService.Create(userDto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPatch("{id}")]
    [Authorize]
    [EnableRateLimiting("HardyEndpoint")]
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
    [Authorize]
    

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("user id not found in token");

        var idUser = Guid.Parse(idUserToken);

        await _userService.Delete(id, idUser, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id}/make-admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<ActionResult> HandleAdmin(Guid id, CancellationToken cancellationToken)
{
    
    Guid currentUserId = Guid.Parse(User.FindFirst("id")!.Value);

    await _userService.HandleAdmin(
        id,
        currentUserId,
        cancellationToken);

    return Ok("User updated as administrator");
}




}