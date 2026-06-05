// using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Application.DTOs.Statistic.Request;
using Application.DTOs.Statistic.Response;

using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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


    [Authorize]
    [HttpGet("getFavoriteGender")]
    public async Task<ActionResult<string>> GetFavoriteGender( CancellationToken cancellationToken)
    {
                  var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("User ID not found in token.");

        var idUser = Guid.Parse(idUserToken);
        var result = await _statisticService.GetFavoriteGender(idUser, cancellationToken);
        return Ok(result);


    }

    [Authorize]
    [HttpGet("getFavoriteSong")]
    public async Task<ActionResult<GetFavoriteSongResponse>> GetFavoriteSong( CancellationToken cancellationToken)
    {
        var idUserToken = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("id")?.Value
            ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idUserToken))
            return Unauthorized("User ID not found in token.");

        var idUser = Guid.Parse(idUserToken);

        var result = await _statisticService.GetFavoriteSong(idUser, cancellationToken);
        return Ok(result);
    }


    [HttpGet("getTopSogs")]
    public async Task<ActionResult<GetTopSongsResponse>> GetTopSongs(CancellationToken cancellationToken)
    {   
        var result = await _statisticService.GetTopSongs(cancellationToken);
        return Ok(result);

        
    }


    [HttpGet("getTopArtists")]
    public async Task<ActionResult<List<GetAllResponse>>> GetTopArtists(CancellationToken cancellationToken)
    {   
        var result = await _statisticService.GetTopArtists(cancellationToken);
        return Ok(result);

        
    }    
}
