using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;
using Application.DTOs.ReproductionList.Request;
using Application.DTOs.ReproductionList.Response;

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
    public ActionResult<ReproductionsList> GetById(Guid id)
    {
        var reproductionList = _reproductionListservice.GetById(id);

        return Ok(reproductionList);
    }

    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest reproductionListDto, CancellationToken cancellationToken)
    {
        var list = await _reproductionListservice.Create(reproductionListDto, cancellationToken);
        return Ok(list);
    }

    [HttpPost("{id}/add-song")]
    public async Task<ActionResult> AddSong(Guid listId, Guid songId)
    {

        await _reproductionListservice.AddSong(listId, songId);
        return Ok("cancion agregada a la lista");


    }

    [HttpPost("{id}/remove-song")]
    public async Task<ActionResult> RemoveSong(Guid listId, Guid songId)
    {
        await _reproductionListservice.RemoveSong(listId, songId);
        return Ok("cancion eliminada de la lista");

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _reproductionListservice.Delete(id);
        return Ok("lista borrada");
    }
}