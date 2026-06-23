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
    private readonly ISongService _song;

    public ReviewService(IReviewRepository review, ISongService song)
    {
        _review = review;
        _song = song;
    }

    public async Task<GetByIdResponse> GetById(Guid Id, CancellationToken cancellationToken)
    {
        ValidateId(Id);
        var review = await _review.GetById(Id, cancellationToken);

        if (review is null)
        {
            throw new NotFoundException("Review");
        }

        return new GetByIdResponse
        {
            Id = review.Id,
            IdUser = review.IdUser,
            IdSong = review.IdSong,
            Comment = review.Comment,
            DateCreated = review.DateCreated
        };
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
            throw new FieldEmptyExcepction(nameof(reviewDto));
        }
        ValidateId(reviewDto.IdUser);
        ValidateId(reviewDto.IdSong);

        if (string.IsNullOrWhiteSpace(reviewDto.Comment))
        {
            throw new FieldEmptyExcepction("Comment");
        }
        if (reviewDto.Comment.Length > 800)
        {
            throw new FieldTooLongException("Comment", 800);
        }
        if (reviewDto.Comment.Length < 3)
        {
            throw new FieldIsNotLongException("Comment", 3);
        }
        var songExists = await _song.GetById(reviewDto.IdSong, cancellationToken);
        if (songExists is null)
        {
            throw new NotFoundException("Song");
        }
        var reviewsExist = await _review.GetBySong(reviewDto.IdSong, cancellationToken);
        if (reviewsExist.Any(x => x.IdUser == reviewDto.IdUser))
        {
            throw new AlreadyExistExcepction("Review", songExists.Title);
        }
        var review = new Review(
            reviewDto.IdUser,
            reviewDto.IdSong,
            reviewDto.Comment

        );

        var reviewCreated = await _review.Create(review, cancellationToken);
        return new CreateResponse
        {
            Id = reviewCreated.Id,
            IdUser = reviewCreated.IdUser,
            IdSong = reviewCreated.IdSong,
            Comment = reviewCreated.Comment,
            DateCreated = review.DateCreated
        };
    }

    public async Task<UpdateResponse> Update(Guid Id, Guid IdUser, UpdateRequest reviewDto, CancellationToken cancellationToken)
    {
        ValidateId(Id);
        ValidateId(IdUser);
        if (reviewDto is null)
        {
            throw new FieldEmptyExcepction("Review");
        }
        var existReview = await _review.GetById(Id, cancellationToken);
        if (existReview is null)
        {
            throw new NotFoundException("Review");
        }
        if (existReview.IdUser != IdUser)
        {
            throw new ForbiddenException("Review");
        }

        existReview.UpdateReview(reviewDto.Comment!);



        await _review.Update(existReview, cancellationToken);
        return new UpdateResponse
        {
            Comment = existReview.Comment
        };
    }

    public async Task Delete(Guid Id, Guid IdUser, CancellationToken cancellationToken)
    {
        ValidateId(Id);
        ValidateId(IdUser);
        var review = await _review.GetById(Id, cancellationToken);
        if (review is null)
        {
            throw new FieldEmptyExcepction("Review");
        }
        ValidateId(review.IdUser);

        if (review.IdUser != IdUser)
        {
            throw new ForbiddenException("Review");
        }


        await _review.Delete(Id, cancellationToken);

    }

    private static void ValidateId(Guid id)
    {
        if (id == Guid.Empty)
            throw new FieldEmptyExcepction("Id");
    }
}