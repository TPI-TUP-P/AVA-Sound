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
}