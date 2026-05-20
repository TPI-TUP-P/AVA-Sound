using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Review.Request;
using Application.DTOs.Review.Response;
using Microsoft.AspNetCore.RateLimiting;
namespace Web.Controllers;

[Route("api/review")]
[ApiController]

[EnableRateLimiting("HeavyEndpoint")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{Id}")]


    public ActionResult<List<GetBySongResponse>> GetBySong([FromRoute] Guid Id, CancellationToken cancellationToken)
    {
        return Ok(_reviewService.GetBySong(Id, cancellationToken));
    }

    [HttpPost]
    [EnableRateLimiting("PerUser")]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateRequest reviewDto, CancellationToken cancellationToken)
    {
        await _reviewService.Create(reviewDto, cancellationToken);
        return Ok();
    }

    [HttpDelete("{Id}")]

    public async Task<ActionResult> Delete(Guid Id, CancellationToken cancellationToken)
    {
        await _reviewService.Delete(Id, cancellationToken);

        return Ok();
    }
    [HttpPatch]
    [EnableRateLimiting("PerUser")]
    public async Task<ActionResult<UpdateResponse>> Update(Guid Id, [FromBody] UpdateRequest reviewDto, CancellationToken cancellationToken)
    {
        await _reviewService.Update(Id, reviewDto, cancellationToken);
        return Ok();
    }

}