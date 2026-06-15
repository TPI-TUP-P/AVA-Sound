namespace Domain.Exceptions;


public class InvalidEmailException : DomainException
{
    public InvalidEmailException(string? Email) : base($"The email {Email} is not valid.") { }
}