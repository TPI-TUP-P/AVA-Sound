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
        var artists = await _song.GetTopArtists(cancellationToken);
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

        if(statistic is null)
        {
            throw new NotFoundException("Statistic");
        }

        if(IdUser != statistic.IdUser)
        {
            throw new ForbiddenException();
        }



    
        var idSong = statistic.GetFavoriteSong();


        var song =await _song.GetById(idSong, cancellationToken);

        if(song == null)
        {
            throw new NotFoundException("song");
        }


       return new GetFavoriteSongResponse
        (
            song.IdArtist,
            song.IdAlbum,
            song.Title!,
            song.Gender!,
            song.Duration!,
            song.AudioBig!,
            song.DateUpload,
            song.Views

        );
        
    }


    public async Task<List<GetTopSongsResponse>> GetTopSongs(CancellationToken cancellationToken)
    {
        var songs = await _song.GetTopSongs(cancellationToken);
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
        
        if(statistic is null)
        throw new NotFoundException("Statistic");

        var songs = await _song.GetByIds(statistic.Reproductions.Select(s=> s.IdSong), cancellationToken);

        if(songs.Count == 0)
        {
            throw new NotFoundException("Songs");
        }

          if(IdUser != statistic.IdUser)
        {
            throw new ForbiddenException();
        }


        
        return statistic.GetFavoriteGender(songs);
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
    
        statistic.RegisterViewGender(IdSong);
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
            ArgumentNullException.ThrowIfNull(statisticDto);
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