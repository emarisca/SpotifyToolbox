using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Services.Playlist;

public interface IGetUnplayableItems
{
    Task<List<PlaylistTrack>> GetPlaylistUnplayableItems(string playlistId, string market);
}