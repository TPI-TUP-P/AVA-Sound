using System.Reflection.Metadata;

namespace Domain.Entities;

using Domain.Exceptions;

public class Review
{
    public Guid Id { get; init; }

    public Guid IdUser { get; init; }

    public Guid IdSong { get; init; }

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
            throw new FieldEmptyExcepction("Id");
        }
        if (idsong == Guid.Empty)
        {
            throw new FieldEmptyExcepction("IdSong");
        }
        if (comment is null)
        {
            throw new FieldEmptyExcepction("Comment"); ;
        }
        if (datecreated > DateTime.Now)
        {
            throw new futureDateException();
        }
    }
}