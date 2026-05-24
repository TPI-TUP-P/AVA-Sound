namespace Domain.Exceptions;


public class FieldEmpetyExcepction : DomainException
{
    public FieldEmpetyExcepction(string? Field) : base($"the {Field} is empty.") { }
}