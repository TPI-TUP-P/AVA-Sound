
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
    public List<Song> Songs { get; set; } = new();


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
        Songs = new List<Song>();
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
            throw new Exception("Song already exists");
        Songs.Add(song);
    }

    public void RemoveSong(Song song)
    {
        if (song == null)
            throw new Exception("Song is required");
        var existing = Songs.FirstOrDefault(s => s.Id == song.Id);
        if (existing == null)
            throw new Exception("Song not found");
        Songs.Remove(existing);
    }


    public void UpdateInfo(string name, string description, bool isPublic)
{
    if (string.IsNullOrWhiteSpace(name))
        throw new Exception("name obligatorio");

    if (string.IsNullOrWhiteSpace(description))
        throw new Exception("description obligatorio");

    Name = name;
    Description = description;
    IsPublic = isPublic;
}


}

