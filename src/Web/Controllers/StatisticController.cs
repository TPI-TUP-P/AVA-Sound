// using Microsoft.AspNetCore.Components;
using Application.DTOs.Statistic.Request;
using Application.DTOs.Statistic.Response;

using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;




[Route("api/statistic")]
[ApiController]

public class StatisticController : ControllerBase
{
    private readonly IStatisticService _statisticService;

    public StatisticController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    [HttpGet("{id}")]
    public ActionResult<GetByIdResponse> GetById(Guid id)
    {
        var album = _statisticService.GetById(id);

        return Ok(album);
    }


    [HttpGet]
    public async Task<ActionResult<List<GetAllResponse>>> GetAll()
    {

        var albums = await _statisticService.GetAll();
        return Ok(albums);
    }


    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest statisticDto, CancellationToken cancellationToken)
    {

        return await _statisticService.Create(statisticDto, cancellationToken);

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid id, [FromBody] UpdateRequest statisticDto)
    {
        return await _statisticService.Update(id, statisticDto);

    }



    [HttpDelete("{id}")]

    public async Task<ActionResult> Delete(Guid id)
    {

        await _statisticService.Delete(id);
        return NoContent();
    }

}
