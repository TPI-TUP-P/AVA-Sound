using System.Reflection.Metadata;

namespace Domain.Entities;

public class Review
{
    public Guid id { get; private set; }

    public Guid idUser { get; private set; }

    public Guid idSong { get; private set; }

    public string? Comment { get; set; }

    public DateTime dateCreated { get; set; }

    private Review(){}


    public Review(Guid iduser, Guid idsong, string comment, DateTime datecreated)
    {
        ValidateProperties(iduser, idsong, comment, datecreated);
        id = Guid.NewGuid();
        idUser = iduser;
        idSong = idsong;
        Comment = comment;
        dateCreated = datecreated;
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