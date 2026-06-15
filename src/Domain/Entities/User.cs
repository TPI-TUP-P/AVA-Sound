using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Entities;


public class User
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsArtist { get; set; }

    public bool IsActive { get; private set; }

    public DateTime DateRegister { get; init; }

    public string? Role { get; private set; }

    public ICollection<Song> Songs { get; private set; } =[];
    


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
        Role = role ?? "user";
        IsActive = true;
    }

    private void ValidateProperties(string? name, string? surname, string? email, string? password)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new FieldEmptyExcepction("name");

        if (string.IsNullOrWhiteSpace(surname))
            throw new FieldEmptyExcepction("surname");

        if (string.IsNullOrWhiteSpace(email))
            throw new FieldEmptyExcepction("email");

        string regexEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        bool isValido=Regex.IsMatch(email, regexEmail);
        if (!isValido)
            throw new InvalidEmailException(email);

        if (string.IsNullOrWhiteSpace(password))
            throw new FieldEmptyExcepction("password");

    }

    public void HandleActivate()
    {
        
        if(!IsActive)
        {
            IsActive = true;

        }
        else
        {
            IsActive = false;
        }

    }

    public void UpdateInfo(string name, string surname, string email, string password, bool isArtist)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new FieldEmptyExcepction("name");

        if (string.IsNullOrWhiteSpace(surname))
            throw new FieldEmptyExcepction("surname");

        if (string.IsNullOrWhiteSpace(email))
            throw new FieldEmptyExcepction("email");

        if (string.IsNullOrWhiteSpace(password))
            throw new FieldEmptyExcepction("password");


        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        IsArtist = isArtist;
    }

    public void HandleAdmin()
    {
        if(Role == "admin")
        {
            Role = "user";
        }
        else
        {
            Role = "admin";
        }
    }

    

}