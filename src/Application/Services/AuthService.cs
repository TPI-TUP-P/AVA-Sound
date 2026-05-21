using Application.DTOs.Auth.Request;
using Application.DTOs.Auth.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.Interfaces.IJwtService;
using Application.DTOs.User.Request;
using Domain.Exceptions;
namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }



    public async Task<RegisterResponse> Register (RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        
        var existingUserEmail = await _userRepository.GetByEmail(registerRequest.Email, cancellationToken);


        if(existingUserEmail != null)
        {
            throw new NotFoundException("Email");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

      
        var user = new User(
            registerRequest.Name,
            registerRequest.Surname,
            registerRequest.Email,
            passwordHash,
            registerRequest.IsArtist,
            registerRequest.Role
        );

        await _userRepository.Create(user, cancellationToken);
        
        return new RegisterResponse (
            user.Name,
            user.Surname,
            user.Email,
            user.IsArtist,
            user.Role  
        );

    }


    public async Task<LoginResponse> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
    

        var user = await _userRepository.GetByEmail(loginRequest.Email, cancellationToken);

        if (user == null)
        {   
            throw new NotFoundException("User");
        }

        var validatePassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

        if (!validatePassword)
        {
            throw new InvalidCredentialsException();
        }

        var createRequest = new CreateRequest(
            user.Name!,
            user.Surname!,
            user.Email!,
            user.Password!,
            user.IsArtist,
            user.Role!
        )
        {
            Id = user.Id
        };

        var token = _jwtService.GenerateToken(createRequest);
        
            

        return new LoginResponse(
            token: token,  
            email: user.Email,
            role: user.Role!
        );
        


    }
   
}