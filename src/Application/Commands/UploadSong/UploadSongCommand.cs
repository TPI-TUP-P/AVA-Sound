// Application/Songs/Commands/UploadSong/UploadSongCommand.cs
public record UploadSongCommand(
    Guid IdArtist,
    Guid? IdAlbum,
    string Title,
    string Gender,
    string Duration,
    Stream AudioStream,
    string FileName,
    string ContentType
);