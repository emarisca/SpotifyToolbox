using Microsoft.AspNetCore.Mvc;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class RemoveItemsRequest
{
    [FromHeader]
    public string Authorization { get; set; }
    [FromRoute(Name = "playlist_id")]
    public string PlaylistId { get; set; }
    [FromBody]
    public RemoveItemsRequestBody Body { get; set; }
}

public class RemoveItemsRequestBody
{
    public List<string> Tracks { get; set; }
}
