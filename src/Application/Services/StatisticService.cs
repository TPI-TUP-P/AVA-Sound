using Application.DTOs.Statistic.Request;
using Application.DTOs.Statistic.Response;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
namespace Application.Services;

public class StatisticService : IStatisticService
{
    private IStatisticRepository _statistic;
    public StatisticService(IStatisticRepository statistic)
    {
        _statistic = statistic;
    }

    public async Task<List<GetAllResponse>> GetAll()
    {
        var statistics = await _statistic.GetAll();
        return statistics.Select(statistic => new GetAllResponse
        (
            statistic.Id,
            statistic.IdUser,
            statistic.SongTop,
            statistic.FavoriteGender,
            statistic.TotalReproductions
        )).ToList();

    }


    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("The ID is empty");
        }

        var statistic = await _statistic.GetById(Id, cancellationToken);

        if (statistic == null)
        {
            throw new Exception("There are no statistics");
        }


        return new GetByIdResponse
        (
            statistic.Id,
            statistic.IdUser,
            statistic.SongTop,
            statistic.FavoriteGender,
            statistic.TotalReproductions

        );
    }



    public async Task<CreateResponse> Create(CreateRequest statisticDto, CancellationToken cancellationToken)
    {
        if (statisticDto == null)
        {
            throw new Exception("The table is empty");
        }


        var statisticData = new Statistic(
            Guid.NewGuid(),
            statisticDto.IdUser,
            statisticDto.SongTop,
            statisticDto.FavoriteGender,
            statisticDto.TotalReproductions
        );




        var statisticCreated = await _statistic.Create(statisticData, cancellationToken);

        return new CreateResponse(
            statisticCreated.Id,
            statisticCreated.IdUser,
            statisticCreated.SongTop,
            statisticCreated.FavoriteGender,
            statisticCreated.TotalReproductions
        );


    }




    // public async Task<UpdateResponse> Update(Guid id,UpdateRequest statisticDto)
    // {
    //     var existingStatistic = await _statistic.GetById(id);

    //     if(existingStatistic== null)
    //     {
    //         throw new Exception("La estadistica no existe");
    //     }

    //     // existingStatistic.IdUser = statistic.IdUser;

    //     existingStatistic.SongTop = statisticDto.SongTop;
    //     existingStatistic.FavoriteGender = statisticDto.FavoriteGender;
    //     existingStatistic.TotalReproductions = statisticDto.TotalReproductions;

    //     await _statistic.Update(
    //         existingStatistic.Id,
    //         existingStatistic
    //     );


    //     return new UpdateResponse (
    //         existingStatistic.Id,
    //         existingStatistic.IdUser,
    //         existingStatistic.SongTop,
    //         existingStatistic.FavoriteGender,
    //         existingStatistic.TotalReproductions
    //     );


    //     }


    public async Task<UpdateResponse> Update(Guid Id, UpdateRequest statisticDto, CancellationToken cancellationToken)
    {
        var existingAlbum = await _statistic.GetById(Id, cancellationToken);
        if (existingAlbum == null)
        {
            throw new Exception("The album does not exist");
        }

        if (statisticDto == null)
        {
            throw new Exception("The album is empty");
        }


        existingAlbum.SongTop = statisticDto.SongTop;
        existingAlbum.FavoriteGender = statisticDto.FavoriteGender;
        existingAlbum.TotalReproductions = statisticDto.TotalReproductions;


        await _statistic.Update(

            existingAlbum, cancellationToken
        );


        return new UpdateResponse
        (
            existingAlbum.Id,
            existingAlbum.IdUser,
            existingAlbum.SongTop,
            existingAlbum.FavoriteGender,
            existingAlbum.TotalReproductions

        );
    }

    public async Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("The ID is empty");
        }

        await _statistic.Delete(Id, cancellationToken);
    }
}