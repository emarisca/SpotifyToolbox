using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class DuplicateItemsResponse : IResponse<DuplicatePlaylistItem>
{
    public IEnumerable<DuplicatePlaylistItem> Data { get; set; }
    public DuplicateItemsResponse(IEnumerable<DuplicatePlaylistItem> data)
    {
        Data = data;
    }
}
