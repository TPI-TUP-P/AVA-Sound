using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/review")]
[ApiController]

public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{Id}")]

    public ActionResult<List<Review>> GetBySong([FromRoute] Guid Id)
    {
        return Ok(_reviewService.GetBySong(Id));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Review review)
    {
        await _reviewService.Create(review);
        return Ok();
    }

    [HttpDelete("{Id}")]

    public async Task<ActionResult> Delete(Guid Id)
    {
        await _reviewService.Delete(Id);

        return Ok();
    }
    [HttpPatch]

    public async Task<ActionResult<InfoUser>> Update([FromBody] Review review)
    {
        await _reviewService.Update(review);
        return Ok();
    }

}