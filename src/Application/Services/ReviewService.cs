using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _review;

    public ReviewService(IReviewRepository review)
    {
        _review = review;
    }


    public Task<List<Review>> GetBySong(Guid Id)
    // For my future self, the id refers to the song id
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("id is empety");
        }

        return _review.GetBySong(Id);
    }

    public Task Create(Review review)
    {
        if (review == null)
        {
            throw new Exception("Review si null");
        }
        return _review.Create(review);
    }

    public Task Update(Review review)
    {
        return _review.Update(review);
    }

    public Task Delete(Guid Id)
    {
        return _review.Delete(Id);
    }
}