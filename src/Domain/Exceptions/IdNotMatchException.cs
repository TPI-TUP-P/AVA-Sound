namespace Domain.Exceptions;


public class IdNotMatchException : DomainException
{
    public IdNotMatchException() : base($"The IDs do not match.") { }
}