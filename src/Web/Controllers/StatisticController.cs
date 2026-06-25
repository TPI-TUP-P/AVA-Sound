// using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Application.DTOs.Statistic.Response;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Web.Controllers;




[Route("api/[controller]")]
[ApiController]

public class StatisticController(IStatisticService _statisticService) : ControllerBase
{
    // private readonly IStatisticService _statisticService;

    // public StatisticController(IStatisticService statisticService)
    // {
    //     _statisticService = statisticService;
    // }


    [HttpGet("getFavoriteGender")]
    [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]

    public async Task<ActionResult<string>> GetFavoriteGender( CancellationToken cancellationToken)
    {
                  var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
             ?? User.FindFirst("id")?.Value;
            

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("User ID not found in token.");

        var idUser = Guid.Parse(idUserToken);
        var result = await _statisticService.GetFavoriteGender(idUser, cancellationToken);
        return Ok(result);


    }

    [HttpGet("getFavoriteSong")]
    [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]

    public async Task<ActionResult<GetFavoriteSongResponse>> GetFavoriteSong( CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
             ?? User.FindFirst("id")?.Value;
            

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("User ID not found in token.");

        var idUser = Guid.Parse(idUserToken);

        var result = await _statisticService.GetFavoriteSong(idUser, cancellationToken);
        return Ok(result);
    }


    [HttpGet("getTopSogs")]
    // [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]
    
    public async Task<ActionResult<GetTopSongsResponse>> GetTopSongs(CancellationToken cancellationToken)
    {   
        var result = await _statisticService.GetTopSongs(cancellationToken);
        return Ok(result);

        
    }


    [HttpGet("getTopArtists")]
    // [Authorize]
    [EnableRateLimiting("HeavyEndpoint")]

    public async Task<ActionResult<List<GetAllResponse>>> GetTopArtists(CancellationToken cancellationToken)
    {   
        var result = await _statisticService.GetTopArtists(cancellationToken);
        return Ok(result);

        
    }    
}
