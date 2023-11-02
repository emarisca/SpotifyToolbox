using System.Collections.Generic;
using AutoMapper;
using SpotifyAPI.Web;
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

        var _spotifyClient = new SpotifyClient(config);

        var userPlaylists = await _spotifyClient.Playlists
            .CurrentUsers(new PlaylistCurrentUsersRequest { Limit = limit, Offset = offset });
        if (userPlaylists != null && userPlaylists.Items != null)
        {
            result = userPlaylists.Items.Select(x => _mapper.Map<Playlist>(x)).ToList();
        }

        return result;
    }
}
