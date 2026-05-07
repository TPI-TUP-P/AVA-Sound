namespace Domain.Exceptions;


public class SongAlredyExistAlbumExcepction : DomainException
{
    public SongAlredyExistAlbumExcepction(Guid IdSong) : base($"Esta cancion {IdSong} ya se encuentra en la lista") {} 
}