namespace Domain.Exceptions;

public class FieldTooLongException : DomainException
{
    public FieldTooLongException(string? Field, int Number) : base($"The {Field} must not contain more than {Number} characters.") { }
}



