using Application.DTOs.Auth.Request;
using Application.DTOs.Auth.Response;
using Application.Interfaces;
using Domain.Entities;
using BCrypt.Net;
using Domain.Interfaces;
using Application.Interfaces.IJwtService;
using Application.DTOs.User.Request;
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
        if (registerRequest.Email == null || registerRequest.Password == null)
        {
            throw new Exception("Your email address and password are required");
        }
        
        var existingUserEmail = await _userRepository.GetByEmail(registerRequest.Email, cancellationToken);


        if(existingUserEmail != null)
        {
            throw new Exception("The email address is already registered");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

        if (registerRequest.Name == null || registerRequest.Surname == null || registerRequest.Role == null)
        {
            throw new Exception("All fields are required");
        }
        
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
        if (loginRequest.Email == null || loginRequest.Password == null)
        {
            throw new Exception("Your email address and password are required");
        }
        

        var user = await _userRepository.GetByEmail(loginRequest.Email, cancellationToken);

        if (user == null)
        {
            throw new Exception("Invalid credentials");
        }

        var validatePassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);

        if (!validatePassword)
        {
            throw new Exception("Invalid credentials");
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
            role: user.Role
        );
        


    }
   
}