using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.InfoUser.Request;
using Application.DTOs.InfoUser.Response;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
namespace Web.Controllers;

[ApiController]

[Route("api/infoUser")]
[Authorize]
[EnableRateLimiting("HeavyEndpoint")]
public class InfoUserController : ControllerBase
{
    private readonly IInfoUserService _infouservice;

    public InfoUserController(IInfoUserService infouserservice)
    {
        _infouservice = infouserservice;
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetByIdResponse>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return Ok(await _infouservice.GetById(Id, cancellationToken));
    }

    [HttpPatch("{Id}")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid Id, [FromBody] UpdateRequest infouserDto, CancellationToken cancellationToken)
    {
        return Ok(await _infouservice.Update(Id, infouserDto, cancellationToken));
    }
    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest infouserDto, CancellationToken cancellationToken)
    {
        var created = await _infouservice.Create(infouserDto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = created.IdUser }, created);
    }

    [HttpDelete("{Id}")]

    public async Task<ActionResult> Delete(Guid Id, CancellationToken cancellationToken)
    {
        await _infouservice.Delete(Id, cancellationToken);

        return NoContent();
    }
}