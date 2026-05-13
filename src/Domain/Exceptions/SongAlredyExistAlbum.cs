using Domain.Entities;

namespace Domain.Exceptions;


public class SongAlredyExistAlbumExcepction : DomainException
{
    public SongAlredyExistAlbumExcepction(Song song) : base($"la cancion {song.Title} ya se encuentra en la lista") {} 
}