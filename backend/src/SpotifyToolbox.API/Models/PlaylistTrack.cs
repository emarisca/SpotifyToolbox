namespace SpotifyToolbox.API.Models;

public class PlaylistTrack
{
    public DateTime? AddedAt { get; set; }
    public int Position { get; set; }
    public Track Track { get; set; } 
}
