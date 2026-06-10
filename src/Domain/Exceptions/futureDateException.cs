using Domain.Exceptions;

public class futureDateException : DomainException
{
    public futureDateException() : base($"The dates cannot be future.", 422) { }
}


