namespace Application.Interfaces;

using Application.DTOs.Statistic.Request;
using Application.DTOs.Statistic.Response;

public interface IStatisticService
{
    Task<List<GetAllResponse>> GetAll();
    Task<GetByIdResponse> GetById(Guid Id);
    Task<CreateResponse> Create(CreateRequest statisticDto, CancellationToken cancellationToken);
    Task<UpdateResponse> Update(Guid Id, UpdateRequest statisticDto);
    Task Delete(Guid Id);

}