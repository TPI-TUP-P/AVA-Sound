// using Microsoft.AspNetCore.Components;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;




[Route("api/album")]
[ApiController]

public class AlbumController : ControllerBase
{
    
    

    private static readonly List<Album> _albums = new();


    [HttpGet]
    public ActionResult <List<Album>> GetAll()
    {
        return Ok(new List<Album>());        
    }


    [HttpPost]
    public ActionResult Create(Album album)
    {
        _albums.Add(album);

        return Ok(album);
    }
    
}
