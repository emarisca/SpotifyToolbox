using Microsoft.AspNetCore.Mvc;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class RemoveItemsRequest
{
    [FromRoute(Name = "playlist_id")]
    public string PlaylistId { get; set; }
    [FromBody]
    public RemoveItemsRequestBody Body { get; set; }
}

public class RemoveItemsRequestBody
{
    public List<RemovePlaylistItem> Tracks { get; set; }
}

public class RemovePlaylistItem
{
    public string Uri { get; set; }
    public List<int> Positions { get; set; }
}