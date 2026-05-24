using Domain.Exceptions;
namespace Domain.Entities;



public class Album
{
    public Guid Id { get; init; }
    public Guid IdArtist { get; init; }
    public string Title { get; set; }
    public DateTime ReleasteDate { get; set; }
    public string? FrontPage { get; set; }
    public string? Description { get; set; }


    // private readonly List<Guid> _songs = [];

    private readonly List<Song> _songs = [];
    public IReadOnlyCollection<Song> Songs => _songs.AsReadOnly();
    // public List<Song> Songs {get; private set;} = new();








    public Album(Guid idArtist, string title, DateTime releasteDate, string? frontPage, string? description)
    {
        ValidateProperties(idArtist, title, releasteDate, frontPage, description);
        Id = Guid.NewGuid();
        IdArtist = idArtist;
        Title = title;
        ReleasteDate = releasteDate;
        FrontPage = frontPage ;
        Description = description ;


    }

    public void AddSong(Song song)
    {
        if (Songs.Contains(song))
        {
            throw new SongAlredyExistAlbumExcepction(song.Title);
        }

        _songs.Add(song);
    }


    public void DeleteSong(Song song)
    {
        if (!Songs.Contains(song))
        {
            throw new NotFoundException("Song");
        }
        _songs.Remove(song);

    }

    private void ValidateProperties(Guid idArtist, string title, DateTime releasteDate, string? frontPage, string? description)
    {
        if (idArtist == Guid.Empty)
        {
            throw new Exception("The IdArtist cannot be empty");
        }
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new Exception("The title cannot be left blank ");
        }
        if (releasteDate > DateTime.Now)
        {
            throw new Exception("The release date cannot be in the future");
        }



        if (description != null)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new Exception("The description cannot be empty");
            }
        }

    }



}