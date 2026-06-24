namespace Application.DTOs.ReproductionList.Response;


public class DeleteSongResponse
{
    public Guid ReproductionListId { get; set; }
    public Guid SongId { get; set; }

    public DeleteSongResponse(Guid reproductionListId, Guid songId)
    {
        ReproductionListId = reproductionListId;
        SongId = songId;
    }

}