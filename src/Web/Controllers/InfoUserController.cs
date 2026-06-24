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

[Route("api/[controller]")]
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
    public async Task<ActionResult<GetByIdResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _infouservice.GetById(id, cancellationToken));
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest infouserDto, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User.FindFirst("id")?.Value
                        ?? User.FindFirst("sub")?.Value;
        if (idUserToken is null)
        {
            throw new FieldEmptyExcepction("Id From token");
        }
        var idUser = Guid.Parse(idUserToken);
        return Ok(await _infouservice.Update(id, idUser, infouserDto, cancellationToken));
    }
    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest infouserDto, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                       ?? User.FindFirst("id")?.Value
                       ?? User.FindFirst("sub")?.Value;
        if (idUserToken is null)
        {
            throw new FieldEmptyExcepction("Id From token");
        }
        var idUser = Guid.Parse(idUserToken);
        var created = await _infouservice.Create(infouserDto, idUser, cancellationToken);

        return CreatedAtAction(
    nameof(GetById),
    new { id = created.Id },
    created);
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