using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs.Review.Request;
using Application.DTOs.Review.Response;
namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _review;

    public ReviewService(IReviewRepository review)
    {
        _review = review;
    }


    public async Task<List<GetBySongResponse>> GetBySong(Guid Id)
    // For my future self, the id refers to the song id
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("id is empety");
        }
        var review = await _review.GetBySong(Id);
        // The r refers to reviews
        return review.Select(r => new GetBySongResponse
        {
            IdUser = r.IdUser,
            IdSong = r.IdSong,
            Comment = r.Comment,
            DateCreated = r.DateCreated
        }).ToList();
    }

    public async Task<CreateResponse> Create(CreateRequest reviewDto)
    {
        if (reviewDto is null)
        {
            throw new Exception("Review is null");
        }
        if (reviewDto.IdUser == Guid.Empty || reviewDto.IdSong == Guid.Empty)
        {
            throw new Exception("Not all fields are correct.");
        }
        if (reviewDto.Comment is null)
        {
            throw new Exception("The comment cannot be empty");
        }


        var review = new Review(
            reviewDto.IdUser,
            reviewDto.IdSong,
            reviewDto.Comment,
            reviewDto.DateCreated
        );

        var reviewCreated = await _review.Create(review);
        return new CreateResponse
        {
            IdUser = reviewCreated.IdUser,
            IdSong = reviewCreated.IdSong,
            Comment = reviewCreated.Comment,
            DateCreated = review.DateCreated
        };
    }

    public async Task<UpdateResponse> Update(Guid Id, UpdateRequest reviewDto)
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("empty id");
        }
        if (reviewDto is null)
        {
            throw new Exception("review is empety.");
        }
        var existReview = await _review.GetById(Id);
        if (reviewDto.Comment != null && reviewDto.Comment.Length > 3)
        {
            existReview.Comment = reviewDto.Comment;
        }
        else
        {
            throw new Exception("It must have at least 3 characters.");
        }

        await _review.Update(existReview);
        return new UpdateResponse
        {
            Comment = existReview.Comment
        };
    }

    public Task Delete(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            throw new Exception("id empety.");
        }
        return _review.Delete(Id);
    }
}