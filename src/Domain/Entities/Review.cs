using System.Reflection.Metadata;

namespace Domain.Entities;

using Domain.Exceptions;

public class Review
{
    public Guid Id { get; init; }

    public Guid IdUser { get; init; }

    public Guid IdSong { get; init; }

    public string Comment { get; set; } = string.Empty;

    public DateTime DateCreated { get; init; }

    private Review() { }


    public Review(Guid iduser, Guid idsong, string comment)
    {
        ValidateProperties(iduser, idsong, comment);
        Id = Guid.NewGuid();
        IdUser = iduser;
        IdSong = idsong;
        Comment = comment;
        DateCreated = DateTime.UtcNow;
    }

    private void ValidateProperties(Guid iduser, Guid idsong, string comment)
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

    }


    public void UpdateReview(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new FieldEmptyExcepction("Comment");
        }

        if (comment.Length < 3)
        {
            throw new FieldIsNotLongException("Comment", 3);
        }

        if (comment.Length > 800)
        {
            throw new FieldTooLongException("Comment", 800);
        }

        Comment = comment;
    }
}