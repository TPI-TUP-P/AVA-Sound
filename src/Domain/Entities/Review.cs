using System.Reflection.Metadata;

namespace Domain.Entities;

public class Review
{
    public Guid Id { get; private set; }

    public Guid IdUser { get; private set; }

    public Guid IdSong { get; private set; }

    public string? Comment { get; set; }

    public DateTime DateCreated { get; set; }

    private Review() { }


    public Review(Guid iduser, Guid idsong, string comment, DateTime datecreated)
    {
        ValidateProperties(iduser, idsong, comment, datecreated);
        Id = Guid.NewGuid();
        IdUser = iduser;
        IdSong = idsong;
        Comment = comment;
        DateCreated = datecreated;
    }

    private void ValidateProperties(Guid iduser, Guid idsong, string comment, DateTime datecreated)
    {
        if (iduser == Guid.Empty)
        {
            throw new Exception("The user ID is empty.");
        }
        if (idsong == Guid.Empty)
        {
            throw new Exception("The song ID is empty.");
        }
        if (comment is null)
        {
            throw new Exception("The comment cannot be empty.");
        }
        if (datecreated > DateTime.Now)
        {
            throw new Exception("The date cannot be in the future.");
        }
    }
}