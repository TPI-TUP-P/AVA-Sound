
namespace Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }

    protected DomainException(string message) : base(message)
    {
        StatusCode = 400;
    }
        
}