using SpotifyAPI.Web;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Lib;

public interface ISpotifyClientWrapper
{
    Task<Paging<FullPlaylist>> GetUserPlaylists(int limit = 50, int offset = 0);
    Task<List<PlaylistTrack>> GetPlaylistItems(string playlistId, string market = default, int limit = 100, int offset = 0);
    Task<List<PlaylistTrack>> GetPlaylistItemsAll(string playlistId, string market = default);
    Task<string> RemovePlaylistItems(string playlistId, PlaylistRemoveItemsRequest request);
    Task<User> GetCurrentUser();
    public string GetAuthorizationUri();
    public Task GetCallback(string code);
}
