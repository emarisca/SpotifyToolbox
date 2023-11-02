using SpotifyAPI.Web;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Lib;

public interface ISpotifyClientWrapper
{
    Task<List<Playlist>> GetUserPlaylists(string token, int limit = 50, int offset = 0);
}
