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

    public async Task<List<Track>> GetPlaylistItems(string token, string playlistId, int limit, int offset)
    {
        var result = new List<Track>();
        var config = SpotifyClientConfig.CreateDefault(token)
            .WithRetryHandler(new SimpleRetryHandler() { RetryTimes = 2, RetryAfter = TimeSpan.FromSeconds(1), TooManyRequestsConsumesARetry = false });
        var spotifyClient = new SpotifyClient(config);

        var request = new PlaylistGetItemsRequest(PlaylistGetItemsRequest.AdditionalTypes.Track)
        {
            Limit = limit,
            Offset = offset
        };

        var playlistItems = await spotifyClient.Playlists.GetItems(playlistId, request);
        if (playlistItems != null && playlistItems.Items != null)
        {
            result = playlistItems.Items.Select(x => _mapper.Map<Track>(x.Track)).ToList();
        }

        return result;
        //PlaylistTrack
        //FullTrack
        //playlistItems.
    }
}
