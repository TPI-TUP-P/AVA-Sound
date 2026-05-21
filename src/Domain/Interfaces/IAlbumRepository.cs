using Domain.Entities;

namespace Domain.Interfaces;


public interface IAlbumRepository : IRepository<Album>
{
   Task<List<Album>> GetAll();

   Task<List<Album>> GetAllByArtist(Guid idArtist, CancellationToken cancellationToken);
   Task<Album> AddSong(Guid id, Song song);
   
}