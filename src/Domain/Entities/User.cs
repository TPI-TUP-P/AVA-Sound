using System.Runtime.CompilerServices;

namespace Domain.Entities;


public class User
{
    public Guid Id { get; private set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public bool IsArtist { get; set; }

    public DateTime DateRegister { get; private set; }

    public string? Role { get; private set; }



    public User(string name, string surname, string email, string password, bool isArtist, string role)
    {
        ValidateProperties(name, surname, email, password);
        Id = Guid.NewGuid();
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        IsArtist = isArtist;
        DateRegister = DateTime.Now;
        Role = role;
    }

    private void ValidateProperties(string? name, string? surname, string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("name is required");

        if (string.IsNullOrWhiteSpace(surname))
            throw new Exception("surname is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("email is required");

        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("password is required");

    }


    public void UpdateInfo(string name, string surname, string email, string password, bool isArtist)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("name is required");

        if (string.IsNullOrWhiteSpace(surname))
            throw new Exception("surname is required");

        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("email is required");

        if (string.IsNullOrWhiteSpace(password))
            throw new Exception("password is required");


        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        IsArtist = isArtist;
    }

    public void MakeAdmin()
    {
        Role = "admin";
    }

}