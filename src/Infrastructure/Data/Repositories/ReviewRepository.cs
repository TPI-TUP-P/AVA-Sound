namespace Infrastructure.Data.Repositories;

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationContext _context;

    public ReviewRepository(ApplicationContext context)
    {
        _context = context;
    }
    public async Task<List<Review>> GetBySong(Guid Id)
    {
        return await _context.Reviews
        .Where(r => r.IdSong == Id)
        .ToListAsync();

    }
    public async Task<Review> GetById(Guid Id)
    {
        return await _context.Reviews.FirstAsync(r => r.Id == Id);

    }
    public async Task<Review> Update(Review review)
    {
        var existing = await _context.Reviews.FindAsync(review.Id);

        if (existing is null)
        {
            throw new KeyNotFoundException($"La reseña con el ID {review.Id} no fue encontrada.");
        }
        existing.Comment = review.Comment;
        await _context.SaveChangesAsync();
        return review;
    }



    public async Task<Review> Create(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return review;
    }
    public async Task Delete(Guid Id)
    {
        var review = await _context.Reviews.FindAsync(Id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
        }
        await _context.SaveChangesAsync();
    }
}
