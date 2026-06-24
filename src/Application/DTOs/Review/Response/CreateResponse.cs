namespace Application.DTOs.Review.Response;

public class CreateResponse
{
    public Guid Id { get; init; }
    public Guid IdUser { get; init; }

    public Guid IdSong { get; init; }

    public string? Comment { get; set; }
    public DateTime DateCreated { get; set; }

    public CreateResponse(Guid id, Guid idUser, Guid idSong, string comment, DateTime dateCreated)
    {
        Id = id;
        IdUser = idUser;
        IdSong = idSong;
        Comment = comment;
        DateCreated = dateCreated;
    }
}