using Domain.Exceptions;

namespace Domain.Entities;

public class InfoUser
{
    public Guid Id { get; init; }

    public Guid IdUser { get; init; }

    public string? ProfilePicture { get; set; }

    public string? Biography { get; set; }
    public string? Country { get; set; }

    private InfoUser() { }

    public InfoUser(Guid iduser, string profilepicture, string biography, string country)
    {
        ValidateProperties(iduser, profilepicture, biography, country);
        Id = Guid.NewGuid();
        IdUser = iduser;
        ProfilePicture = profilepicture;
        Biography = biography;
        Country = country;
    }
    private void ValidateProperties(Guid iduser, string profilepicture, string biography, string country)
    {
        if (iduser == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }
        if (profilepicture is null)
        {
            throw new FieldEmptyExcepction("ProfilePicture");
        }
        if (country is null)
        {
            throw new FieldEmptyExcepction("Country");
        }
        if (biography is null)
        {
            throw new FieldEmptyExcepction("Biography");
        }
    }
}