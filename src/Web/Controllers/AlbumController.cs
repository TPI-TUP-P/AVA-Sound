// using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace Web.Controllers;



[ApiController]
[Route("api/[controller]")]


public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var album = await _albumService.GetById(id, cancellationToken);

        return Ok(album);
    }


    [HttpGet]
    public async Task<ActionResult<List<GetAllResponse>>> GetAll()
    {

        var albums = await _albumService.GetAll();
        return Ok(albums);
    }

    
    [Authorize]
    [HttpPost]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest albumDto, CancellationToken cancellationToken)
    {

        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                  ?? User.FindFirst("id")?.Value 
                  ?? User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(idUserToken))
        {
            return Unauthorized("User ID not found in token.");
        }
        
        var idUser = Guid.Parse(idUserToken);
     
        
        var album=  await _albumService.Create(albumDto, idUser ,cancellationToken);
        return CreatedAtAction(nameof(GetById),new { id = album.Id }, album);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest albumDto, CancellationToken cancellationToken)
    {
        return await _albumService.Update(id, albumDto, cancellationToken);

    }

    [Authorize]
    [HttpPost("{id}/add-song/{idSong}")]
    public async Task<ActionResult<GetByIdResponse>> AddSong(Guid id, Guid idSong, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _albumService.AddSong(id, idSong, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, result
            );

        }

        catch (Exception ex)
        {
            return this.StatusCode(500, ex.Message);
        }
    }



    [Authorize]
    [HttpDelete("{id}")]

    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
             var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                  ?? User.FindFirst("id")?.Value 
                  ?? User.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(idUserToken))
        {
            return Unauthorized("User ID not found in token.");
        }
        
        var idUser = Guid.Parse(idUserToken);
        
        await _albumService.Delete(id, idUser,cancellationToken);
        return NoContent();
    }

}
