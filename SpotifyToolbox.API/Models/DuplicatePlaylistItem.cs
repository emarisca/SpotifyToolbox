namespace SpotifyToolbox.API.Models
{
    public class DuplicatePlaylistItem
    {
        public string DuplicateName { get; set; }
        public IEnumerable<PlaylistTrack> Tracks { get; set; }
    }
}
