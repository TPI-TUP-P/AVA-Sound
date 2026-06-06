namespace Application.Interfaces;

using Application.DTOs.Album.Request;
using Application.DTOs.Album.Response;

public interface        IAlbumService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);
    Task<List<GetAllResponse>> GetAll();

    Task<List<GetAllResponse>> GetAllByArtist(Guid idArtist, CancellationToken cancellationToken);
    Task<CreateResponse> Create(CreateRequest albumDto,Guid idUser,CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest albumDto, CancellationToken cancellationToken);
    Task Delete(Guid id,Guid idUser, CancellationToken cancellationToken);

    Task<GetByIdResponse> AddSong(Guid id, Guid idSong,Guid idUser,  CancellationToken cancellationToken);

    Task<GetByIdResponse> DeleteSong(Guid id, Guid idSong, Guid idUser, CancellationToken cancellationToken);

}


