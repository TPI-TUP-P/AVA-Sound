namespace Application.DTOs.Review.Response;

public class GetByIdResponse
{
    public Guid Id;
    public Guid IdUser { get; init; }

    public Guid IdSong { get; init; }

    public string? Comment { get; set; }

    public DateTime DateCreated { get; init; }

    public GetByIdResponse(Guid id, Guid idUser, Guid idSong, string comment, DateTime dateCreated)
    {
        Id = id;
        IdUser = idUser;
        IdSong = idSong;
        Comment = comment;
        DateCreated = dateCreated;
    }
}