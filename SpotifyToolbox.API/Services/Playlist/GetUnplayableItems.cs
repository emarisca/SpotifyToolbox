using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Services.Playlist;

public class GetUnplayableItems : IGetUnplayableItems
{
    private readonly ISpotifyClientWrapper _spotifyClientWrapper;
    public GetUnplayableItems(ISpotifyClientWrapper spotifyClientWrapper)
    {
        _spotifyClientWrapper = spotifyClientWrapper;
    }

    public async Task<IEnumerable<PlaylistTrack>> GetPlaylistUnplayableItems(string token, string playlistId)
    {
        var response = new List<PlaylistTrack>();

        var playlistItems = await _spotifyClientWrapper.GetPlaylistItemsAll(token, playlistId);
        if (playlistItems != null)
        {
            response = playlistItems.Where(x => x.Track.IsPlayable == false).ToList();
        }

        return response;

    }
}
