using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth.Request;


public class RegisterRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Surname is required")]
    public string Surname { get; set; }= string.Empty;
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set;} = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
    public bool IsArtist { get; set; }
    
}