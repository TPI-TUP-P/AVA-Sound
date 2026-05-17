using Domain.Entities;

namespace Domain.Exceptions;


public class ReviewAlreadyExistExcepction : DomainException
{
    public ReviewAlreadyExistExcepction(string? songTitle) : base($"You already have a review on the song '{songTitle}'") { }
}