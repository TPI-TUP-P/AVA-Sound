using Application.DTOs.Auth.Request;
using Application.DTOs.Auth.Response;

namespace Application.Interfaces;



public interface IAuthService
{
    Task<LoginResponse> Login(LoginRequest loginRequest, CancellationToken cancellationToken);
    Task<RegisterResponse> Register(RegisterRequest registerRequest,
     CancellationToken cancellationToken);

}