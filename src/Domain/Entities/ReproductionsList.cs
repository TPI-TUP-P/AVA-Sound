
using Domain.Exceptions;

namespace Domain.Entities;


public class ReproductionsList
{
    public Guid Id { get; init; }
    public Guid IdUser { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public DateTime Creation { get; set; }
    // public string? SoundList {get; set;}

    private readonly List<Song> _songs = [];
    public IReadOnlyCollection<Song> Songs => _songs.AsReadOnly();
    // public List<Song> Songs {get; private set;} = new();


    private ReproductionsList() { }

    public ReproductionsList(Guid idUser, string name, string description, bool isPublic)
    {
        ValidateProperties(idUser, name, description);
        Id = Guid.NewGuid();
        IdUser = idUser;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Creation = DateTime.Now;
        // SoundList = soundList;
    }

    private void ValidateProperties(Guid idUser, string name, string description)
    {
        if (idUser == Guid.Empty)
            throw new FieldEmptyExcepction("User");
        if (string.IsNullOrWhiteSpace(name))
            throw new FieldEmptyExcepction("Name");
        if (string.IsNullOrWhiteSpace(description))
            throw new FieldEmptyExcepction("Description");


    }

    public void AddSong(Song song)
    {
        if (song == null)
            throw new FieldEmptyExcepction("Song");
        if (Songs.Any(s => s.Id == song.Id))
            throw new AlreadyExistExcepction("Song", "ReproductionsList ");
        _songs.Add(song);
    }

    public void RemoveSong(Song song)
    {
        if (song == null)
            throw new FieldEmptyExcepction("Song");
        var existing = Songs.FirstOrDefault(s => s.Id == song.Id);
        if (existing == null)
            throw new NotFoundException("Song");
        _songs.Remove(existing);
    }


    public void UpdateInfo(string name, string description, bool isPublic)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new FieldEmptyExcepction("Name");

        if (string.IsNullOrWhiteSpace(description))
            throw new FieldEmptyExcepction("Description");

        Name = name;
        Description = description;
        IsPublic = isPublic;
    }


}

