using Application.DTOs.Auth.Request;
using Application.DTOs.Auth.Response;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.Interfaces.IJwtService;
using Application.DTOs.User.Request;
using Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IStatisticRepository _statisticRepository;

    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService, IStatisticRepository statisticRepository)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _statisticRepository = statisticRepository;
    }



    private static bool IsValidEmail(string email )
    {
        return new EmailAddressAttribute().IsValid(email);
    }

    private static bool IsValidPassword(string password)
    {
        return Regex.IsMatch(
            password,
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$"
        );
    }

    public async Task<RegisterResponse> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
    {
        
        if(!IsValidEmail(registerRequest.Email))
        {
            throw new InvalidEmailException(registerRequest.Email);
        }

        if(!IsValidPassword(registerRequest.Password))
        {
            throw new InvalidPasswordException();
        }

        var existingUserEmail = await _userRepository.GetByEmail(registerRequest.Email, cancellationToken);

        if (existingUserEmail != null)
        {
            throw new AlreadyExistExcepction("Email");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

        var user = new User(
            registerRequest.Name,
            registerRequest.Surname,
            registerRequest.Email,
            passwordHash,
            registerRequest.IsArtist
        );

        await _userRepository.Create(user, cancellationToken);

        var statistic =new Statistic(
            user.Id
        );

        await _statisticRepository.Create(statistic, cancellationToken);

        return new RegisterResponse(
            user.Name,
            user.Surname,
            user.Email,
            user.IsArtist,
            user.Role
        );

    }


    public async Task<LoginResponse> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
    {


        var user = await _userRepository.GetByEmail(loginRequest.Email!, cancellationToken);

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
            user.Id,
            user.Name,
            user.Surname,
            user.Email,
            user.Password,
            user.IsArtist,
            user.Role
        );

        var token = _jwtService.GenerateToken(createRequest);



        return new LoginResponse(
            token: token,
            email: user.Email!,
            role: user.Role!
        );
    }

}