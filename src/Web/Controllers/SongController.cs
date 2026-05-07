using Application.DTOs.Song.Request;
using Application.DTOs.Song.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/Song")]
[ApiController]

public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService=songService;
    }

    [HttpGet("{id}")]

    public ActionResult<GetByAlbumResponse> GetById(Guid Id)
    {
        var song=_songService.GetById(Id);
        return Ok(song);
    }

    [HttpGet]

    public async Task<ActionResult<List<GetAllResponse>>> GetAll()
    {
        var songs=await _songService.GetAll();
        return Ok(songs);
    }

    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create(CreateRequest songDto)
    {
        var song= await _songService.Create(songDto);
        return Ok(songDto);
    }

    [HttpPut("{id}")]

    public async Task<ActionResult<UpdateResponse>> Update(Guid id, UpdateRequest songDto)
    {
        var song = await _songService.Update(id, songDto);
        return Ok(song);
    }


    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(Guid id)
    {
        await _songService.Delete(id);
        return NoContent();
    }
}