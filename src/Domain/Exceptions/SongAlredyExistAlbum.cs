using Domain.Entities;

namespace Domain.Exceptions;


public class SongAlredyExistAlbumExcepction : DomainException
{
    public SongAlredyExistAlbumExcepction(string songTitle) : base($"la cancion {songTitle} ya se encuentra en la lista") {} 
}