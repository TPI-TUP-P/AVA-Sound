namespace Application.Interfaces;

using Application.DTOs.Statistic.Request;
using Application.DTOs.Statistic.Response;

public interface IStatisticService
{
    Task<List<GetAllResponse>> GetAll();
    Task<CreateResponse> Create(CreateRequest statisticDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest statisticDto, CancellationToken cancellationToken);
    
    Task<List<GetTopSongsResponse>> GetTopSongs(CancellationToken cancellationToken);
    Task Delete(Guid Id, CancellationToken cancellationToken);

}