
using Domain.Exceptions;

namespace Domain.Entities;


public class ReproductionsList
{
    public Guid Id {get; private set;}
    public Guid IdUser {get; private set;}
    public string Name {get; set;}=null!;
    public string Description {get; set;}=null!;
    public bool IsPublic {get; set;}
    public DateTime Creation {get; set;}
    // public string? SoundList {get; set;}
  
    private readonly List<Song> _songs = [];
    public IReadOnlyCollection<Song> Songs => _songs.AsReadOnly();
    // public List<Song> Songs {get; private set;} = new();


    private ReproductionsList(){}

    public ReproductionsList(Guid idUser, string name, string description, bool isPublic)
    {
        ValidateProperties(idUser, name, description);
        Id=Guid.NewGuid();
        IdUser = idUser;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Creation = DateTime.Now;
        // SoundList = soundList;
    }

    private void ValidateProperties(Guid idUser, string name, string description)
    {
        if (idUser==Guid.Empty)
            throw new Exception("user is required");
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("name is requiered");
        if (string.IsNullOrWhiteSpace(description))
            throw new Exception("description is requiered");
        
        
    }

    public void AddSong(Song song)
    {
        if (song == null)
            throw new Exception("Song is required");
        if (Songs.Any(s => s.Id == song.Id))
            throw new AlreadyExistExcepction("Song", "ReproductionsList ");
        _songs.Add(song);
    }

    public void RemoveSong(Song song)
    {
        if (song == null)
            throw new Exception("Song is required");
        var existing = Songs.FirstOrDefault(s => s.Id == song.Id);
        if (existing == null)
            throw new Exception("Song not found");
        _songs.Remove(existing);
    }


    public void UpdateInfo(string name, string description, bool isPublic)
{
    if (string.IsNullOrWhiteSpace(name))
        throw new Exception("name is required");

    if (string.IsNullOrWhiteSpace(description))
        throw new Exception("description is required");

    Name = name;
    Description = description;
    IsPublic = isPublic;
}


}

