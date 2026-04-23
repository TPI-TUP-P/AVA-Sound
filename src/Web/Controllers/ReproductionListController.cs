using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Entities;

namespace Web.Controllers;

[ApiController]
[Route("api/reproduction-list")]
public class ReproductionListController : ControllerBase
{
    private readonly IReproductionListService _service;

    public ReproductionListController(IReproductionListService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReproductionsList>> GetById(Guid id)
    {
        var list = await _service.GetById(id);

        if (list == null)
            return NotFound(new { message = "List not found" });

        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ReproductionsList list)
    {
        var result = await _service.Create(list);
        return Ok(result);
    }

    [HttpPost("{id}/add-song")]
    public async Task<ActionResult> AddSong(Guid id, Song song)
    {
        try
        {
            var result = await _service.AddSong(id, song);
            return Ok(result);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Song not found" });
        }
    }

    [HttpPost("{id}/remove-song")]
    public async Task<ActionResult> DeleteSong(Guid id, Song song)
    {
        try
        {
            var result = await _service.DeleteSong(id, song);
            return Ok(result);
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Song not found" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _service.Delete(id);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound(new { message = "List not found" });
        }
    }
}