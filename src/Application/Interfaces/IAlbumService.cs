namespace Application.Interfaces;

using Application.DTOs.Album;
using Domain.Entities;

public interface IAlbumService
{
    Task <Album> GetById(Guid id);
    Task <List<Album>> GetAll();
    Task  Create(CreateAlbumDto albumDto);
    Task  Update(UpdateAlbumDto albumDto);
    Task Delete(Guid id);
}


