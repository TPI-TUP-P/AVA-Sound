using Domain.Exceptions;

public class ForbiddenException : DomainException
{
    public ForbiddenException() : base("You are not authorized to perform this action"){}
    public ForbiddenException(string entity) : base($"You are not authorized to modify {entity}", 403) {}
}
        
     
