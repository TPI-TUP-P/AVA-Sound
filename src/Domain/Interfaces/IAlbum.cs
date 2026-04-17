using Domain.Entities;

namespace Domain.Interfaces;


public interface IAlbum
{
   Task<Album?> GetById(Guid id);
   Task<List<Album>> GetAll();
   Task Add(Album album);
   Task Update(Album album);
   Task Delete(Guid id);
}