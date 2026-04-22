using Domain.Entities;

namespace Domain.Interfaces;

public interface IReviewRepository : IRepository<Review>
{

    Task<List<Review>> GetBySong(Guid idSong);


}