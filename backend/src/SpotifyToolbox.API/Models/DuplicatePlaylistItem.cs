namespace SpotifyToolbox.API.Models;

public class DuplicatePlaylistItem
{
    public string DuplicateName { get; set; }
    public List<PlaylistTrack> Tracks { get; set; }
}
