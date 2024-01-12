using Microsoft.AspNetCore.Mvc;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class ItemsRequest
{
    [FromQuery]
    public string PlaylistId { get; set; }
    [FromQuery]
    public int Offset { get; set; }
    [FromQuery]
    public int Limit { get; set; }
}
