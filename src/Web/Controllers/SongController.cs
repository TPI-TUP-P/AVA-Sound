using System.Security.Claims;
using Application.DTOs.Song.Request;
using Application.DTOs.Song.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]

public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<GetByIdResponse>> GetById(Guid Id, CancellationToken cancellationToken)
    {
        var response = await _songService.GetById(Id, cancellationToken);
        return Ok(response);
    }

    [HttpGet]

    public async Task<ActionResult<PagerSongResponse<GetByIdResponse>>> GetAll([FromBody] PagerRequest pagerRequest,CancellationToken cancellationToken)
    {
        var songs = await _songService.GetAll(pagerRequest, cancellationToken);
        return Ok(songs);
    }

    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest songDto, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                          ?? User.FindFirst("id")?.Value 
                          ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
        {
            return Unauthorized("User ID not found in token.");
        }

        var idUser = Guid.Parse(idUserToken);

        var song = await _songService.Create(songDto, idUser, cancellationToken);
        return Ok(song);
    }

    [HttpPatch("{id}")]

    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest songDto, CancellationToken cancellationToken)
    {
        var song = await _songService.Update(id, songDto, cancellationToken);
        return Ok(song);
    }


    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _songService.Delete(id, cancellationToken);
        return NoContent();
    }
}