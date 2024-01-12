using Serilog;
using SpotifyToolbox.API.Extensions;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Services.Playlist;

public class GetDuplicateItems : IGetDuplicateItems
{
    private readonly ISpotifyClientWrapper _spotifyClientWrapper;
    public GetDuplicateItems(ISpotifyClientWrapper spotifyClientWrapper)
    {
        _spotifyClientWrapper = spotifyClientWrapper;
    }

    public async Task<List<DuplicatePlaylistItem>> GetPlaylistDuplicateItems(string playlistId, string market)
    {
        var result = new List<DuplicatePlaylistItem>();

        var playlistItems = await _spotifyClientWrapper.GetPlaylistItemsAll(playlistId, market);
        Log.Information("Finished fetching all playlist items");

        result = playlistItems
            .GroupBy(x => new
            {
                TrackName = x.Track.Name,
                DurationMs = x.Track.DurationMs,
                Artists = x.Track.Artists.Select(i => i.Id).Stringify(",")
            })
            .Select(x => new
            {
                TrackName = x.Key.TrackName,
                DurationMs = x.Key.DurationMs,
                Artists = x.Key.Artists,
                Count = x.Count()
            })
            .Where(x => x.Count > 1)
            .Select(x => new DuplicatePlaylistItem
             {
                 DuplicateName = x.TrackName,
                 Tracks = playlistItems
                            .Where(i => i.Track.Name == x.TrackName
                                && i.Track.DurationMs == x.DurationMs
                                && i.Track.Artists.Select(i => i.Id).Stringify(",") == x.Artists)
                            .ToList()
             }).ToList();

        return result;
    }
}
