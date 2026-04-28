namespace Domain.Entities;


public class User
{
    public Guid Id {get; private set;}
    public string? Name {get; private set;}
    public string? Surname {get; private set;}
    public string? Email { get; private set;}
    public string? Password { get; private set;}
    public bool IsArtista {get; private set;}

    public DateTime DateRegister { get; private set;}

    public string? Role { get; private set;}

    private User(){}


    public User(string name, string surname, string email, string password, bool isArtista,string role)
    {
        ValidateProperties(name, surname, email, password, role);
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        IsArtista= isArtista;
        DateRegister = DateTime.Now;
        Role = role;
        
    }

    private void ValidateProperties(string? name, string? surname, string? email, string? password, string? role)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new Exception("name is required");
        
        if (string.IsNullOrWhiteSpace(surname))
            throw new Exception("surname is required");
        
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("email is required");
        
        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("password is required");

        if (string.IsNullOrWhiteSpace(role))
            throw new Exception("user role is required");
        

    }
        

} 