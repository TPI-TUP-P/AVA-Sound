// using Microsoft.AspNetCore.Components;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;




[Route("api/album")]
[ApiController]

public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet("{id}")]
public ActionResult<Album> GetById(Guid id)
{
    var album = _albumService.GetById(id);

    if (album == null)
        return NotFound();

    return Ok(album);
}
    

    [HttpGet]
    public async Task<ActionResult <List<Album>>> GetAll()
    {
       
        var albums = await _albumService.GetAll();
            return Ok(albums);
    }


    [HttpPost ]
    public ActionResult Create([FromBody] Album album)
    {
        _albumService.Create(album);

        return CreatedAtAction(nameof(GetById), new { id = album.Id }, album );
    }
    
}
