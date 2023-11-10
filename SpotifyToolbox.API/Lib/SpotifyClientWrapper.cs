using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json.Linq;
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

    public async Task<List<Playlist>> GetUserPlaylists(string token, int limit, int offset)
    {
        var result = new List<Playlist>();

        var config = SpotifyClientConfig.CreateDefault(token)
            .WithRetryHandler(new SimpleRetryHandler() { RetryTimes = 2, RetryAfter = TimeSpan.FromSeconds(1), TooManyRequestsConsumesARetry = false });

        var spotifyClient = new SpotifyClient(config);

        var userPlaylists = await spotifyClient.Playlists
            .CurrentUsers(new PlaylistCurrentUsersRequest { Limit = limit, Offset = offset });
        if (userPlaylists != null && userPlaylists.Items != null)
        {
            result = userPlaylists.Items.Select(x => _mapper.Map<Playlist>(x)).ToList();
        }

        return result;
    }

    public async Task<List<PlaylistTrack>> GetPlaylistItems(string token, string playlistId, int limit, int offset)
    {
        var result = new List<PlaylistTrack>();
        var config = SpotifyClientConfig.CreateDefault(token)
            .WithRetryHandler(new SimpleRetryHandler() { RetryTimes = 10, RetryAfter = TimeSpan.FromSeconds(1), TooManyRequestsConsumesARetry = false });
        var spotifyClient = new SpotifyClient(config);

        var request = new PlaylistGetItemsRequest(PlaylistGetItemsRequest.AdditionalTypes.Track)
        {
            Limit = limit,
            Offset = offset
        };

        var playlistItems = await spotifyClient.Playlists.GetItems(playlistId, request);
        if (playlistItems != null && playlistItems.Items != null)
        {
            result = playlistItems.Items.Select(x => _mapper.Map<PlaylistTrack>(x)).ToList();
        }

        return result;
    }

    public async Task<List<PlaylistTrack>> GetPlaylistItemsAll(string token, string playlistId)
    {
        var result = new List<PlaylistTrack>();

        int offset = 0;
        bool continueFetching = true;

        while (continueFetching)
        {
            var batchItems = await GetPlaylistItems(token, playlistId, MAX_PLAYLIST_ITEMS, offset);
            if (batchItems != null && batchItems.Count > 0)
            {
                result.AddRange(batchItems);
                offset += MAX_PLAYLIST_ITEMS;
            }
            else
            {
                continueFetching = false;
            }
        }

        return result;
    }
}
