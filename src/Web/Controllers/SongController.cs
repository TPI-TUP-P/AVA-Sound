using System.Security.Claims;
using Application.DTOs.Song.Request;
using Application.DTOs.Song.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs.Song;  // ← el SongUploadRequest

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    // ← solo ISongService, sin el handler
    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await _songService.GetById(id, cancellationToken);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<PagerSongResponse<GetByIdResponse>>> GetAll([FromQuery] PagerRequest pagerRequest, CancellationToken cancellationToken)
    {
        var songs = await _songService.GetAll(pagerRequest, cancellationToken);
        return Ok(songs);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    [DisableRequestSizeLimit]
    public async Task<ActionResult<CreateResponse>> Create([FromForm] SongUploadRequest request, CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("User ID not found in token.");

        var idUser = Guid.Parse(idUserToken);

        using var stream = request.AudioFile.OpenReadStream();

        var songDto = new CreateRequest
        {
            IdAlbum = request.IdAlbum,
            Title = request.Title,
            Gender = request.Gender,
            Duration = request.Duration
        };

        var song = await _songService.Create(
            songDto,
            stream,
            request.AudioFile.FileName,
            request.AudioFile.ContentType,
            idUser,
            cancellationToken
        );

        return Ok(song);
    }

    // ← usa _songService directamente, sin handler
    [HttpGet("{id}/url")]
    public async Task<ActionResult<string>> GetSongUrl(Guid id, CancellationToken cancellationToken)
    {
        var url = await _songService.GetSongUrl(id, cancellationToken);
        return Ok(url);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest songDto, CancellationToken cancellationToken)
    {
        var idUserToken=User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if(string.IsNullOrEmpty(idUserToken))
            return Unauthorized("User id not found in token");

        var idUser=Guid.Parse(idUserToken);
        
        var song = await _songService.Update(id, songDto, idUser,  cancellationToken);
        return Ok(song);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var idUserToken= User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if(string.IsNullOrEmpty(idUserToken))
            return Unauthorized("User id not found in token");

        var idUser=Guid.Parse(idUserToken);

        await _songService.Delete(id, idUser, cancellationToken);
        return NoContent();
    }
}