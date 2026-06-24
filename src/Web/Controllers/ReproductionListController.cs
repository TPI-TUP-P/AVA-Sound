using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using Application.DTOs.ReproductionList.Request;
using Application.DTOs.ReproductionList.Response;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;

namespace Web.Controllers;

[ApiController]
[Route("api/reproduction-list")]
public class ReproductionListController : ControllerBase
{
    private readonly IReproductionListService _reproductionListservice;

    public ReproductionListController(IReproductionListService reproductionListservice)
    {
        _reproductionListservice = reproductionListservice;
    }

    [HttpGet("{id}")]
    [EnableRateLimiting("HeavyEndpoint")]
    public ActionResult<ReproductionsList> GetById(Guid id, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("user id not found in token");

        var idUser = Guid.Parse(idUserToken);
        var reproductionList = _reproductionListservice.GetById(id, idUser, cancellationToken);

        return Ok(reproductionList);
    }

    // [HttpGet]
    // [Authorize(Roles = "Admin")]
    // [EnableRateLimiting("HeavyEndpoint")]
    // public async Task<ActionResult<List<GetAllResponse>>> GetAll(CancellationToken cancellationToken)
    // {
    //     var reproductionLists = await _reproductionListservice.GetAll(cancellationToken);
    //     return Ok(reproductionLists);
    // }

    [HttpPatch("{id}")]
    [Authorize]
    [EnableRateLimiting("HardyEndpoint")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest updateRequest, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("user id not found in token");

        var idUser = Guid.Parse(idUserToken);

        var result = await _reproductionListservice.Update(id, updateRequest, idUser, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost]
    [Authorize]
    [EnableRateLimiting("HardyEndpoint")]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest reproductionListDto, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("id")?.Value
                          ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
        {
            return Unauthorized("User ID not found in token.");
        }
        var idUser = Guid.Parse(idUserToken);

        //reproductionListDto.IdUser = Guid.Parse(idUserToken);


        var list = await _reproductionListservice.Create(idUser, reproductionListDto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = list.Id }, list);
    }

    [HttpGet("/me")]
    [Authorize]
    [EnableRateLimiting("HardyEndpoint")]
    public async Task<ActionResult<List<GetAllResponse>>> GetMyLists(CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                          ?? User.FindFirst("id")?.Value
                          ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
        {
            return Unauthorized("User ID not found in token.");
        }

        var idUser = Guid.Parse(idUserToken);
        var lists = await _reproductionListservice.GetByIdUser(idUser, cancellationToken);
        return Ok(lists);
    }




    [HttpPost("{listId}/add-song/{songId}")]
    [Authorize]
    [EnableRateLimiting("HardyEndpoint")]
    public async Task<ActionResult> AddSong(Guid listId, Guid songId, CancellationToken cancellationToken)
    {

        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("user id not found in token");

        var idUser = Guid.Parse(idUserToken);

        await _reproductionListservice.AddSong(listId, songId, idUser, cancellationToken);
        return Ok("song added to the list");


    }

    [HttpDelete("{listId}/remove-song/{songId}")]
    [Authorize]
    
    public async Task<ActionResult> RemoveSong(Guid listId, Guid songId, CancellationToken cancellationToken)
    {

        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("user id not found in token");

        var idUser = Guid.Parse(idUserToken);


        await _reproductionListservice.RemoveSong(listId, songId, idUser, cancellationToken);
        return NoContent();

    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("user id not found in token");

        var idUser = Guid.Parse(idUserToken);

        await _reproductionListservice.Delete(id, idUser, cancellationToken);
        return NoContent();
    }
}