using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Application.DTOs.InfoUser.Request;
using Application.DTOs.InfoUser.Response;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Domain.Exceptions;
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
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User.FindFirst("id")?.Value
                        ?? User.FindFirst("sub")?.Value;
        if (idUserToken is null)
        {
            throw new FieldEmptyExcepction("Id From token");
        }
        var idUser = Guid.Parse(idUserToken);
        return Ok(await _infouservice.Update(Id, idUser, infouserDto, cancellationToken));
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
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? User.FindFirst("id")?.Value
                       ?? User.FindFirst("sub")?.Value;
        if (idUserToken is null)
        {
            throw new FieldEmptyExcepction("Id From token");
        }
        var idUser = Guid.Parse(idUserToken);
        await _infouservice.Delete(Id, idUser, cancellationToken);

        return NoContent();
    }
}