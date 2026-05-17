using Application.DTOs.Auth.Request;
using Application.DTOs.Auth.Response;
using Application.Interfaces;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;



[Route("api/auth")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        var response = await _authService.Register(registerRequest, cancellationToken);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        var response = await _authService.Login(loginRequest, cancellationToken);
        return Ok(response);
    }
}