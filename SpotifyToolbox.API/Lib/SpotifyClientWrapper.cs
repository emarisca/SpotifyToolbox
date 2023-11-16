using AutoMapper;
using Serilog;
using SpotifyAPI.Web;
using SpotifyToolbox.API.Endpoints.Playlist;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Lib;

public class SpotifyClientWrapper : ISpotifyClientWrapper
{
    protected IMapper _mapper;
    private int MAX_PLAYLIST_ITEMS = 100;

    public SpotifyClientWrapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    private SpotifyClient CreateSpotifyClient(string token)
    {
        var config = SpotifyClientConfig.CreateDefault(token)
           .WithRetryHandler(new SimpleRetryHandler() { RetryTimes = 2, RetryAfter = TimeSpan.FromSeconds(1), TooManyRequestsConsumesARetry = false });

        var spotifyClient = new SpotifyClient(config);
        return spotifyClient;
    }

    public async Task<List<Playlist>> GetUserPlaylists(string token, int limit, int offset)
    {
        var result = new List<Playlist>();

        var spotifyClient = CreateSpotifyClient(token);

        var userPlaylists = await spotifyClient.Playlists
            .CurrentUsers(new PlaylistCurrentUsersRequest { Limit = limit, Offset = offset });
        if (userPlaylists != null && userPlaylists.Items != null)
        {
            result = userPlaylists.Items.Select(x => _mapper.Map<Playlist>(x)).ToList();
        }

        return result;
    }

    public async Task<List<PlaylistTrack>> GetPlaylistItems(string token, string playlistId, string market,  int limit, int offset)
    {
        var result = new List<PlaylistTrack>();

        var spotifyClient = CreateSpotifyClient(token);

        var request = new PlaylistGetItemsRequest(PlaylistGetItemsRequest.AdditionalTypes.Track)
        {
            Limit = limit,
            Offset = offset,
            Market = market
        };

        var playlistItems = await spotifyClient.Playlists.GetItems(playlistId, request);
        if (playlistItems != null && playlistItems.Items != null)
        {
            result = playlistItems.Items.Select(x => _mapper.Map<PlaylistTrack>(x)).ToList();
        }

        return result;
    }

    public async Task<List<PlaylistTrack>> GetPlaylistItemsAll(string token, string playlistId, string market)
    {
        var result = new List<PlaylistTrack>();

        var spotifyClient = CreateSpotifyClient(token);

        var request = new PlaylistGetItemsRequest(PlaylistGetItemsRequest.AdditionalTypes.Track)
        {
            Limit = MAX_PLAYLIST_ITEMS,
            Offset = 0,
            Market = market
        };
        var firstPage = await spotifyClient.Playlists.GetItems(playlistId, request);
        Log.Information("First page finished");
        var allPages = await spotifyClient.PaginateAll(firstPage);
        Log.Information("All pages finished");
        if (allPages != null)
        {
            result = allPages.Select(x => _mapper.Map<PlaylistTrack>(x)).ToList();
        }

        return result;
    }

    public async Task<string> RemovePlaylistItems(string token, string playlistId, IEnumerable<string> tracks)
    {
        string snapshotId = String.Empty;

        var spotifyClient = CreateSpotifyClient(token);

        var request = new PlaylistRemoveItemsRequest
        {
            Tracks = tracks.Select(x => new PlaylistRemoveItemsRequest.Item { Uri = x }).ToList()
        };

        var result = await spotifyClient.Playlists.RemoveItems(playlistId, request);
        if (result != null)
        {
            snapshotId = result.SnapshotId;
        }

        return snapshotId;
    }

    public async Task<User> GetCurrentUser(string token)
    {
        var result = new User();

        var spotifyClient = CreateSpotifyClient(token);

        var currentUser = await spotifyClient.UserProfile.Current();
        result = _mapper.Map<User>(currentUser);

        return result;
    }
}
