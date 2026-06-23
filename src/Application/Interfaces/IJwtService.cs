using Application.DTOs.User.Request;

namespace Application.Interfaces.IJwtService;


public interface IJwtService
{
    string GenerateToken(CreateRequest createRequest);
    // bool ValidateToken(string token);
    
}