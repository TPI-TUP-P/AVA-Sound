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

    public UserRole Role { get; private set;}


    public User(string? name, string? surname, string? email, string? password, bool isArtista,UserRole role)
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

    private void ValidateProperties(string? name, string? surname, string? email, string? password, bool isArtista,UserRole role)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new Exception("el nombre es obligatorio");
        if(name.Length<3)
            throw new Exception("el nombre debe contener minimo 3 caracteres");
        if (string.IsNullOrWhiteSpace(surname))
            throw new Exception("El apellido es obligatorio");
        if (surname.Length < 3)
            throw new Exception("El apellido debe tener al menos 3 caracteres");
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("El email es obligatorio");
        if (!email.Contains("@") || !email.Contains("."))
            throw new Exception("El email no tiene un formato válido");
        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("La contraseña es obligatoria");
        if (password.Length < 6)
            throw new Exception("La contraseña debe tener al menos 6 caracteres");

    }
        
    public enum UserRole
    {
        User = 0,
        Admin = 1
    }

} 