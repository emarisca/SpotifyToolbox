using SpotifyAPI.Web;
using SpotifyToolbox.API.Endpoints.Playlist;

namespace SpotifyToolbox.API.Services.Playlist;

public interface IRemovePlaylistItemsService
{
    Task<string> RemovePlaylistItems(RemoveItemsRequest request);
}
