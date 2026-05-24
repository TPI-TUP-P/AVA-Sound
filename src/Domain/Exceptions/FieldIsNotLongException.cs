namespace Domain.Exceptions;


public class FieldIsNotLongException : DomainException
{
    public FieldIsNotLongException(string? Field, int Number) : base($"The {Field} must have at least {Number} characters.") { }
}