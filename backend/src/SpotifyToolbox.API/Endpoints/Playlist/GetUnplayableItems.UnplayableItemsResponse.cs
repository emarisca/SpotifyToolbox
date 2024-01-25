using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class UnplayableItemsResponse : IResponse<PlaylistTrack>
{
    public IEnumerable<PlaylistTrack> Data { get; set; }
    public int? Total { get; set; }
    public UnplayableItemsResponse(IEnumerable<PlaylistTrack> data)
    {
        Data = data;
    }
}
