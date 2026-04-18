using System.Data.Common;

namespace Domain.Entities;


public class ReproductionsList
{
    public Guid Id {get; private set;}
    public Guid IdUser {get; private set;}
    public string? Name {get; private set;}
    public string? Description {get; private set;}
    public bool IsPublic {get; private set;}
    public DateTime Creation {get; set;}
    public string? SoundList {get; set;}


    public ReproductionsList(Guid idUser, string name, string description, bool isPublic, string soundList)
    {
        ValidateProperties(idUser, name, description, soundList);
        Id=Guid.NewGuid();
        IdUser = idUser;
        Name = name;
        Description = description;
        IsPublic = isPublic;
        Creation = DateTime.Now;
        SoundList = soundList;
    }

    private void ValidateProperties(Guid idUser, string name, string description, string soundList)
    {
        if (idUser==Guid.Empty)
            throw new Exception("user is required");
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("name is requiered");
        if (string.IsNullOrWhiteSpace(description))
            throw new Exception("description is requiered");
        if (string.IsNullOrWhiteSpace(soundList))
            throw new Exception("sound list is required");
        
    }


}

