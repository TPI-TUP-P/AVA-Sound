namespace Application.DTOs.ReproductionList.Request;


public class AddSongRequest
{
    public Guid ReproductionListId { get; set; }
        public Guid SongId { get; set; }
}