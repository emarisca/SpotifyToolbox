namespace SpotifyToolbox.API.Models;

public class Track
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int DurationMs { get; set; }
    public bool IsPlayable { get; set; }
    public string Uri { get; set; }
    public Album Album { get; set; }
    public List<Artist> Artists { get; set; }
}
