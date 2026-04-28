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
    

    [HttpGet]
    public ActionResult <List<Album>> GetAll()
    {
       return Ok(_albumService.GetAll()); 
              
    }


    [HttpPost ]
    public ActionResult Create([FromBody] Album album)
    {
        _albumService.Create(album);

        return CreatedAtAction(nameof(album), new { id = album.Id }, album );
    }
    
}
