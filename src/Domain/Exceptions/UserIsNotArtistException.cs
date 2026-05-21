using Domain.Exceptions;

public class UserIsNotArtistException: DomainException
{
    public UserIsNotArtistException(string message) : base(message){}
}