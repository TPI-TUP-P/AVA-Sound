namespace Application.DTOs.User.Request;

public class CreateRequest
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsArtist { get; set; }

    public string? Role { get; set; }

}




