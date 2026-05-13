// using Microsoft.AspNetCore.Components;
using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;

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
    public ActionResult<GetByIdResponse> GetById(Guid id)
    {
        var album = _albumService.GetById(id);

        return Ok(album);
    }


    [HttpGet]
    public async Task<ActionResult<List<GetAllResponse>>> GetAll()
    {

        var albums = await _albumService.GetAll();
        return Ok(albums);
    }


    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest albumDto, CancellationToken cancellationToken)
    {
        return await _albumService.Create(albumDto, cancellationToken);



    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest albumDto)
    {
        return await _albumService.Update(id, albumDto);

    }



    [HttpDelete("{id}")]

    public async Task<ActionResult> Delete(Guid id)
    {

        await _albumService.Delete(id);
        return NoContent();
    }

}
