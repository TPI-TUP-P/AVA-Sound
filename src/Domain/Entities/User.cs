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


    public User(string name, string surname, string email, string password, bool isArtista,string role)
    {
        ValidateProperties(name, surname, email, password, isArtista, role);
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        IsArtista= isArtista;
        DateRegister = DateTime.Now;
        Role = role;
        
    }

    private void ValidateProperties(string? name, string? surname, string? email, string? password, bool isArtista, string? role)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new Exception("el nombre es obligatorio");
        
        if (string.IsNullOrWhiteSpace(surname))
            throw new Exception("El apellido es obligatorio");
        
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("El email es obligatorio");
        
        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("La contraseña es obligatoria");

        if (!isArtista)
            throw new Exception("el tipo de usuario es obligatorio");

        if (string.IsNullOrWhiteSpace(role))
            throw new Exception("el rol de usuario es obligatorio");
        

    }
        

} 