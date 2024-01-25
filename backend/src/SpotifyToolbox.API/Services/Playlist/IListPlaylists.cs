using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Services.Playlist
{
    public interface IListPlaylists
    {
        Task<ResponseModel<Models.Playlist>> GetUserPlaylists(int limit, int offset);
    }
}
