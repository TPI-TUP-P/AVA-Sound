namespace Application.Interfaces;

using Application.DTOs.Statistic.Response;

public interface IStatisticService
{
    // Task<List<GetAllResponse>> GetAll();

    Task<CreateResponse> RegisterReproductionAsync(Guid IdSong,Guid IdUser, CancellationToken cancellationToken);

    Task<GetFavoriteSongResponse> GetFavoriteSong(Guid IdUser, CancellationToken cancellationToken);
    Task<string> GetFavoriteGender(Guid IdUser, CancellationToken cancellationToken);
    Task<List<GetTopSongsResponse>> GetTopSongs(CancellationToken cancellationToken);

    Task<IEnumerable<GetTopArtistReponse>> GetTopArtists(CancellationToken cancellationToken);


}