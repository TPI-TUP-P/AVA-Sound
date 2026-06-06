using Domain.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException(string entity) : base($"You are not authorized to modify {entity}", 403) {}
}
        
     
