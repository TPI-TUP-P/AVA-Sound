using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.User.Request;
using Application.Interfaces.IJwtService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


///  Desde mi perspectiva pense que es mejor que el token tenga un archivo de service 
///    en vez de tenerlo en el user ya que romperia con la regla de responsabilidad unica.
public class JwtService : IJwtService
{


    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(CreateRequest createRequest)
    {


        // Este apartado coloca los datos que va a contener el token
        List<Claim> claims =
        [
            new Claim (
            ClaimTypes.NameIdentifier,
            createRequest.Id.ToString()
        ),

        new Claim(
            ClaimTypes.Name,
            createRequest.Name ),

        new Claim(
            ClaimTypes.Surname,
            createRequest.Surname),

        new Claim(
            ClaimTypes.Email,
            createRequest.Email),

        new Claim(
            ClaimTypes.Role,
            createRequest.Role!
            ),

        new Claim(
            "IsArtist",
            createRequest.IsArtist.ToString())
        ];

        // Obtener la clave secreta desde la configuración

        string secretKey = _config["JWT_SECRET_KEY"] 
                           ?? _config["Jwt:Key"] 
                           ?? throw new InvalidOperationException("JWT Secret Key is missing in configuration.");

        // Validar que la clave tenga el tamaño minimo requerido para HS256 (lo use para testear un problema que estaba teniendo)

        if (secretKey.Length < 32)
        {
            throw new InvalidOperationException("The JWT key in appsettings.json must be at least 32 characters long to comply with the HS256 algorithm.");
        }


        // Crear la clave de seguridad y las credenciales de firma

        SymmetricSecurityKey key = new(
            Encoding.UTF8.GetBytes(secretKey));

        // Crear las credenciales de firma utilizando el algoritmo HMAC SHA256

        SigningCredentials creds = new(
            key,
            SecurityAlgorithms.HmacSha256);

        // Crear el objeto del token

        JwtSecurityToken token = new(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        // Retorna el token serializado

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    // public bool ValidateToken(string token)
    // {
    //     return !string.IsNullOrEmpty(token);

    // }

}
