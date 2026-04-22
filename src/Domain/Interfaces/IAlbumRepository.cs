using Domain.Entities;

namespace Domain.Interfaces;


public interface IAlbumRepository : IRepository<Album>
{
   Task<List<Album>> GetAll();
  

}