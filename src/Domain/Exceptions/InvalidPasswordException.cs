namespace Domain.Exceptions;


public class InvalidPasswordException : DomainException
{
    public InvalidPasswordException() : base($"The password is not valid "){}
}