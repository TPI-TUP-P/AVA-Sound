namespace Domain.Entities;

public class InfoUser
{
    public Guid Id { get; private set; }

    public Guid IdUser { get; private set; }

    public string? ProfilePicture { get; set; }

    public string? Biography { get; set; }
    public string? Country { get; set; }

    private InfoUser(){}

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
            throw new Exception("The user ID cannot be empty.");
        }
        if (profilepicture is null)
        {
            throw new Exception("The profile picture cannot be empty.");
        }
        if (country is null)
        {
            throw new Exception("The country cannot be empty.");
        }
        if (biography is null)
        {
            throw new Exception("The biography cannot be empty.");
        }
    }
}