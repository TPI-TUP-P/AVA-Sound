namespace Domain.Exceptions;


public class FieldEmptyExcepction : DomainException
{
    public FieldEmptyExcepction(string? Field) : base($"the {Field} is empty.") { }
}