namespace Domain.Entities;



public class Album
{
   public Guid Id {get; private set;}
    public Guid IdArtist {get; private set;}
    public string? Title {get; set;}
    public DateTime ReleasteDate {get; set;}
    public string? FrontPage {get;set;}
    public string? Description {get;set;}

    public Album(Guid idArtist, string? title, DateTime releasteDate, string? frontPage, string? description)
    {
        ValidateProperties(idArtist, title, releasteDate, frontPage, description);
        Id = Guid.NewGuid();
        IdArtist = idArtist;
        Title = title;
        ReleasteDate = releasteDate;
        FrontPage = frontPage;
        Description = description;
    }


    private void ValidateProperties(Guid idArtist, string? title, DateTime releasteDate, string? frontPage, string? description)
    {
        if (idArtist == Guid.Empty)
        {
            throw new Exception("El IdArtist no puede ser vacio");
        }
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new Exception("El titulo no puede estar vacio");
        }
        if (releasteDate > DateTime.Now)
        {
            throw new Exception("La fecha de lanzamiento no puede ser futura");
        }
        if (string.IsNullOrWhiteSpace(frontPage))
        {
            throw new Exception("La portada no puede estar vacia");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new Exception("La descripcion no puede estar vacia");
        }
    }
    


}