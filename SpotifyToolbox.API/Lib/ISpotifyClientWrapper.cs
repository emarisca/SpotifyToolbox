using SpotifyAPI.Web;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Lib;

public interface ISpotifyClientWrapper
{
    Task<List<Playlist>> GetUserPlaylists(string token, int limit = 50, int offset = 0);
    Task<List<PlaylistTrack>> GetPlaylistItems(string token, string playlistId, string market = default, int limit = 100, int offset = 0);
    Task<List<PlaylistTrack>> GetPlaylistItemsAll(string token, string playlistId, string market = default);
    Task<string> RemovePlaylistItems(string token, string playlistId, IEnumerable<string> tracks);
    Task<User> GetCurrentUser(string token);
    public Uri GetAuthorizationUri();
    public Task GetCallback(string code);
}
