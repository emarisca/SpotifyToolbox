using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Services.Playlist;

public interface IGetDuplicateItems
{
    Task<IEnumerable<DuplicatePlaylistItem>> GetPlaylistDuplicateItems(string playlistId, string market);
}
