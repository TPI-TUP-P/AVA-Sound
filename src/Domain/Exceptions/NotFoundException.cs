using Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string entity) : base($"{entity} was not found") {}
}


