namespace Domain.Exceptions;
public class UserNotActivateException : DomainException
{
    public UserNotActivateException() : base("User is already inactive.") { }
}