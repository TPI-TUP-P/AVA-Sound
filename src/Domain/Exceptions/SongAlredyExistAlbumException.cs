using Domain.Entities;

namespace Domain.Exceptions;


public class SongAlredyExistAlbumExcepction : DomainException
{
    public SongAlredyExistAlbumExcepction(string songTitle) : base($"The song {songTitle} is already on the list") {} 
}