using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.DTOs.Review.Request;
using Application.DTOs.Review.Response;
using Domain.Exceptions;
namespace Application.Services;


public class ReviewService : IReviewService
{
    private readonly IReviewRepository _review;
    private readonly ISongRepository _song;

    public ReviewService(IReviewRepository review, ISongRepository song)
    {
        _review = review;
        _song = song;
    }


    public async Task<List<GetBySongResponse>> GetBySong(Guid Id, CancellationToken cancellationToken)
    // For my future self, the id refers to the song id
    {
        if (Id == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }
        var review = await _review.GetBySong(Id, cancellationToken);
        // The r refers to reviews
        return review.Select(r => new GetBySongResponse
        {
            IdUser = r.IdUser,
            IdSong = r.IdSong,
            Comment = r.Comment,
            DateCreated = r.DateCreated
        }).ToList();
    }

    public async Task<CreateResponse> Create(CreateRequest reviewDto, CancellationToken cancellationToken)
    {
        if (reviewDto is null)
        {
            throw new Exception("Review is null");
        }
        if (reviewDto.IdUser == Guid.Empty || reviewDto.IdSong == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }
        if (string.IsNullOrWhiteSpace(reviewDto.Comment))
        {
            throw new FieldEmptyExcepction("Comment");
        }
        if (reviewDto.Comment.Length > 800)
        {
            throw new ArgumentException("The comment cannot exceed 800 characters.");
        }
        if (reviewDto.Comment.Length < 3)
        {
            throw new FieldIsNotLongException("Comment", 3);
        }
        var songExists = await _song.GetById(reviewDto.IdSong, cancellationToken);
        if (songExists is null)
        {
            throw new NotFoundException("Song"); ;
        }
        var reviewsExist = await _review.GetBySong(reviewDto.IdSong, cancellationToken);
        if (reviewsExist.Any(x => x.IdUser == reviewDto.IdUser))
        {
            throw new AlreadyExistExcepction("Review", songExists.Title);
        }




        var review = new Review(
            reviewDto.IdUser,
            reviewDto.IdSong,
            reviewDto.Comment,
            reviewDto.DateCreated
        );

        var reviewCreated = await _review.Create(review, cancellationToken);
        return new CreateResponse
        {
            IdUser = reviewCreated.IdUser,
            IdSong = reviewCreated.IdSong,
            Comment = reviewCreated.Comment,
            DateCreated = review.DateCreated
        };
    }

    public async Task<UpdateResponse> Update(Guid Id, UpdateRequest reviewDto, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }
        if (reviewDto is null)
        {
            throw new FieldEmptyExcepction("Review");
        }
        var existReview = await _review.GetById(Id, cancellationToken);
        if (reviewDto.Comment != null && reviewDto.Comment.Length > 3)
        {
            existReview.Comment = reviewDto.Comment;
        }
        else
        {
            throw new FieldIsNotLongException("Comment", 3);
        }

        await _review.Update(existReview, cancellationToken);
        return new UpdateResponse
        {
            Comment = existReview.Comment
        };
    }

    public Task Delete(Guid Id, CancellationToken cancellationToken)
    {
        if (Id == Guid.Empty)
        {
            throw new FieldEmptyExcepction("Id");
        }
        return _review.Delete(Id, cancellationToken);
    }
}