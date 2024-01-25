using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class ItemsResponse : IResponse<PlaylistTrack>
{
    public IEnumerable<Models.PlaylistTrack> Data { get; }
    public int? Total { get; set; }

    public ItemsResponse(IEnumerable<Models.PlaylistTrack> data)
    {
        Data = data;
    }
}
