
namespace Domain.Exceptions;


public class AlreadyExistExcepction : DomainException
{
    public AlreadyExistExcepction(string? name, string? entity) : base($" already have a {entity} for '{name}'") { }
}