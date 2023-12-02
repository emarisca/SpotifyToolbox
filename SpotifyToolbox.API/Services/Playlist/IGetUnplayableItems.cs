using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Services.Playlist;

public interface IGetUnplayableItems
{
    Task<IEnumerable<PlaylistTrack>> GetPlaylistUnplayableItems(string playlistId, string market);
}