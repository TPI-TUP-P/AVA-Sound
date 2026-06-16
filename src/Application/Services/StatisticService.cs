using Application.DTOs.Statistic.Request;
using Application.DTOs.Statistic.Response;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
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


        if(IdUser != statistic.IdUser)
        {
            throw new Exception("You don't have permission to access these statistics");
        }


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

    public async  Task<string> GetFavoriteGender(Guid IdUser, CancellationToken cancellationToken)
    {
        var statistic = await _statistic.GetByIdUser(IdUser, cancellationToken);
        
        if(IdUser != statistic.IdUser)
        {
            throw new Exception("You don't have permission to access these statistics");
        }


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
            statisticDto.IdUser);

        var statisticCreated = await _statistic.Create(statisticData, cancellationToken);

        return new CreateResponse(
            statisticCreated.Id,
            statisticCreated.IdUser,
            statisticCreated.Reproductions
        );


    }




}