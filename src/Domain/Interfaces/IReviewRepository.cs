using Domain.Entities;

namespace Domain.Interfaces;

public interface IReviewRepository
{
    Task<Review> GetById(Guid idReview);

    Task<List<Review>> GetBySong(Guid idSong);

    Task Add(Review review);

    Task Update(Review review);

    Task Delete(Guid idReview);

}