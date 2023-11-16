using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class RemoveItemsResponse
{
    public string Data { get; set; }
    public RemoveItemsResponse(string data)
    {
        Data = data;
    }
}
