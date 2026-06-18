// using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace Web.Controllers;



[ApiController]
[Route("api/[controller]")]


public class AlbumController(IAlbumService _albumService) : ControllerBase
{
    // private readonly IAlbumService _albumService;

    // public AlbumController(IAlbumService albumService)
    // {
    //     _albumService = albumService;
    // }

    [HttpGet("{id}")]
    [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<GetByIdResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var album = await _albumService.GetById(id, cancellationToken);

        return Ok(album);
    }


    [HttpGet("artist/{idArtist}")]
    [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<List<GetAllResponse>>> GetAllBydArtist (Guid idArtist, CancellationToken cancellationToken)
    {
        var albums = await _albumService.GetAllByArtist(idArtist, cancellationToken);
        return Ok(albums);
    ;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<List<GetAllResponse>>> GetAll()
    {

        var albums = await _albumService.GetAll();
        return Ok(albums);
    }

    
    [HttpPost]
     [Authorize]
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
        var album = await _albumService.Create(albumDto, idUser, cancellationToken);
        
        return CreatedAtAction(nameof(GetById), new { id = album.Id }, album);
        
    }
    [HttpPut("{id}")]
    [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest albumDto, CancellationToken cancellationToken)
    {
        return await _albumService.Update(id, albumDto, cancellationToken);

    }

    [HttpPost("{id}/add-song/{idSong}")]
    [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]

    public async Task<ActionResult<GetByIdResponse>> AddSong(Guid id, Guid idSong, CancellationToken cancellationToken)
    {
            var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(string.IsNullOrEmpty(idUserToken))
        {
            return Unauthorized("User ID not found in token.");
        }
        var idUser = Guid.Parse(idUserToken);
        
      
            var result = await _albumService.AddSong(id, idSong, idUser,cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, result
            );
}

    [HttpPost("{id}/delete-song/{idSong}")]
    [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]
    public async Task<ActionResult<GetByIdResponse>> DeleteSong(Guid id, Guid idSong, CancellationToken cancellationToken)
    {   
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(string.IsNullOrEmpty(idUserToken))
        {
            return Unauthorized("User ID not found in token.");
        }

        var idUser = Guid.Parse(idUserToken);
        

        var result = await _albumService.DeleteSong(id, idSong,idUser, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, result);
    }


    [HttpDelete("{id}")]
    [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]
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
