
namespace Domain.Exceptions;


public class SongNotInAlbumException : DomainException
{
    public SongNotInAlbumException(string title) : base($"The song {title} does not belong to the album.") {}
}