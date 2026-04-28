namespace Application.Interfaces;

using Domain.Entities;

public interface IReviewService
{
    Task<List<Review>> GetBySong(Guid Id);
    Task Create(Review review);
    Task Update(Review review);
    Task Delete(Guid id);
}