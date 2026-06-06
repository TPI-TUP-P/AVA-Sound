using Application.DTOs.Statistic.Request;
using Application.DTOs.Statistic.Response;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using Application.Validators;
namespace Application.Services;

public class StatisticService : IStatisticService
{
    private IStatisticRepository _statistic;
    private ISongRepository _song;

    public StatisticService(IStatisticRepository statistic, ISongRepository song)
    {
        _statistic = statistic;
        _song = song;
    }

    // public async Task<List<GetAllResponse>> GetAll()
    // {
    //     var statistics = await _statistic.GetAll();
    //     return statistics.Select(statistic => new GetAllResponse
    //     (
    //         statistic.Id,
    //         statistic.IdUser,
    //         statistic.SongTop,
    //         statistic.FavoriteGender,
    //         statistic.TotalReproductioByGender
    //     )).ToList();

    // }


    public async Task<IEnumerable<GetTopArtistReponse>> GetTopArtists(CancellationToken cancellationToken)
    {
        var artists = await _statistic.GetTopArtist(cancellationToken);
        return artists.GroupBy(s=> new {s.IdArtist,NameArtist = s.Artist.Name})
        .Select(g=> new GetTopArtistReponse(
            g.Key.IdArtist,
            g.Key.NameArtist,
            g.Count(),
            g.Sum(s => s.Views)
            
        )
        
        ).OrderByDescending(a => a.TotalViews).Take(10);
    
    }


    public async Task<GetFavoriteSongResponse> GetFavoriteSong(Guid IdUser, CancellationToken cancellationToken)
    {
        var statistic = await _statistic.GetByIdUser(IdUser, cancellationToken);


        AuthValidator.ValidateOwner(statistic.IdUser, IdUser, "You don't have permission to access these statistics");

        if(statistic is null)
        {
            throw new NotFoundException("Statistic");
        }
        var idSong = statistic.GetFavoriteSong();
        var song =await _song.GetById(idSong, cancellationToken);
       
       return new GetFavoriteSongResponse
        (
            song.IdArtist,
            song.IdAlbum,
            song.Title!,
            song.Gender!,
            song.Duration!
            ,song.AudioBig!,
            song.DateUpload,
            song.Views

        );
        
    }


    public async Task<List<GetTopSongsResponse>> GetTopSongs(CancellationToken cancellationToken)
    {
        var songs = await _statistic.GetTopSongs(cancellationToken);
        return songs.Select(song => new GetTopSongsResponse
        (
            song.Id,
            song.Title,
            song.Gender,
            song.Duration,
            song.Views,
            song.IdArtist,
            song.IdAlbum
        )).ToList();
    }

    // public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    // {
    //     if (Id == Guid.Empty)
    //     {
    //         throw new Exception("The ID is empty");
    //     }

    //     var statistic = await _statistic.GetById(Id, cancellationToken);

    //     if (statistic == null)
    //     {
    //         throw new Exception("There are no statistics");
    //     }


    //     return new GetByIdResponse
    //     (
    //         statistic.Id,
    //         statistic.IdUser,
    //         statistic.SongTop,
    //         statistic.FavoriteGender,
    //         statistic.TotalReproductions

    //     );
    // }


    public async  Task<string> GetFavoriteGender(Guid IdUser, CancellationToken cancellationToken)
    {
        var statistic = await _statistic.GetByIdUser(IdUser, cancellationToken);
        
        AuthValidator.ValidateOwner(statistic.IdUser, IdUser, "You don't have permission to access these statistics");

        if(statistic is null)
        {
            throw new NotFoundException("Statistic");
        }






        return statistic.GetFavoriteGender();
    }


    public async Task<CreateResponse> RegisterReproductionAsync(Guid IdSong,Guid IdUser,  CancellationToken cancellationToken)
    {
        var statistic = await _statistic.GetByIdUser(IdUser, cancellationToken);
        var song = await _song.GetById(IdSong, cancellationToken);
    
        if(statistic is null)
        {
            throw new NotFoundException("Statistic");
        }

        if(song is null )
        {
            throw new NotFoundException("Song");
        }
    
        statistic.RegisterViewGender(IdSong, song.Gender);
        await _statistic.UpdateStatistic(statistic, cancellationToken);

        return new CreateResponse
        (
            statistic.Id,
            statistic.IdUser,
            statistic.Reproductions
        );
    

         
    }

    public async Task<CreateResponse> Create(CreateRequest statisticDto, CancellationToken cancellationToken)
    {
        if (statisticDto == null)
        {
            throw new Exception("The table is empty");
        }


        var statisticData = new Statistic(
            statisticDto.IdUser
           
                    );




        var statisticCreated = await _statistic.Create(statisticData, cancellationToken);

        return new CreateResponse(
            statisticCreated.Id,
            statisticCreated.IdUser,
            statisticCreated.Reproductions
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


        await _statistic.Update(

            existingAlbum, cancellationToken
        );


        return new UpdateResponse
        (
            existingAlbum.Id,
            existingAlbum.IdUser, 
            existingAlbum.Reproductions

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