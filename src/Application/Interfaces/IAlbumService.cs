namespace Application.Interfaces;
using Domain.Entities;

public interface IAlbumService
{
    Task <Album> GetById(Guid id);
    Task <List<Album>> GetAll();
    Task  Create(Album album);
    Task  Update(Album album);
    Task Delete(Guid id);
}


