namespace Application.Interfaces;

using Application.DTOs.Review.Request;
using Application.DTOs.Review.Response;
using Domain.Entities;

public interface IReviewService
{
    Task<List<GetBySongResponse>> GetBySong(Guid Id);
    Task Create(CreateRequest reviewDto);
    Task Update(Guid Id, UpdateRequest reviewDto);
    Task Delete(Guid id);
}