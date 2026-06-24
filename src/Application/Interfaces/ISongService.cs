namespace Application.Interfaces;

using Application.DTOs.Song.Response;
using Application.DTOs.Song.Request;
using Domain.Entities;

public interface ISongService
{
    Task<GetByIdResponse> GetById(Guid id, CancellationToken cancellationToken);

    Task<PagerSongResponse<GetByIdResponse>> GetAll(CancellationToken cancellationToken);
    Task<CreateResponse> Create(
        CreateRequest songDto,
        Stream audioStream,
        string fileName,
        string contentType,
        Guid idUser,
        CancellationToken cancellationToken
    );
    Task<UpdateResponse> Update(Guid Id, UpdateRequest songDto, Guid idUser, CancellationToken cancellationToken);
    Task Delete(Guid id, Guid idUser ,CancellationToken cancellationToken);


    Task<string> GetSongUrl(Guid songId,Guid IdUser,  CancellationToken cancellationToken);
}