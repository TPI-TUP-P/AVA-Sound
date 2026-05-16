using Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string entity) : base($"No se encontro {entity}") {}
}


