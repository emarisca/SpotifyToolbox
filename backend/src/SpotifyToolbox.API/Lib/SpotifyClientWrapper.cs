using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Serilog;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using SpotifyToolbox.API.Endpoints.Playlist;
using SpotifyToolbox.API.Models;
using SpotifyToolbox.API.Services;
using System.Xml.Linq;

namespace SpotifyToolbox.API.Lib;

public class SpotifyClientWrapper : ISpotifyClientWrapper
{
    protected IMapper _mapper;
    protected ISessionService _sessionService;
    protected IOptions<AuthSettings> _authSettings;
    private int MAX_PLAYLIST_ITEMS = 100;

    public SpotifyClientWrapper(
        IMapper mapper, 
        ISessionService sessionService, 
        IOptions<AuthSettings> authSettings)
    {
        _mapper = mapper;
        _sessionService = sessionService;
        _authSettings = authSettings;
    }

    private SpotifyClient CreateSpotifyClient()
    {
        if (isValidAccessToken() == false)
        {
            RefreshAccessToken();
        }

        string accessToken = _sessionService.GetAccessToken();
        if (String.IsNullOrWhiteSpace(accessToken) == false)
        {
            var config = SpotifyClientConfig.CreateDefault(accessToken)
               .WithRetryHandler(new SimpleRetryHandler() { RetryTimes = 2, RetryAfter = TimeSpan.FromSeconds(1), TooManyRequestsConsumesARetry = false });
  
            var spotifyClient = new SpotifyClient(config);
            return spotifyClient;
        }

        return null!;
    }

    /*
    public async Task<List<Playlist>> GetUserPlaylists(int limit, int offset)
    {
        var result = new List<Playlist>();

        var spotifyClient = CreateSpotifyClient();
        if (spotifyClient != null)
        {
            var userPlaylists = await spotifyClient.Playlists
                .CurrentUsers(new PlaylistCurrentUsersRequest { Limit = limit, Offset = offset });
            if (userPlaylists != null && userPlaylists.Items != null)
            {
                result = userPlaylists.Items.Select(x => _mapper.Map<Playlist>(x)).ToList();
            }
        }

        return result;
    }*/

    public async Task<Paging<FullPlaylist>> GetUserPlaylists(int limit, int offset)
    {
        var result = new Paging<FullPlaylist>();

        var spotifyClient = CreateSpotifyClient();
        if (spotifyClient != null)
        {
            result = await spotifyClient.Playlists
                .CurrentUsers(new PlaylistCurrentUsersRequest { Limit = limit, Offset = offset });
        }

        return result;
    }

    public async Task<List<PlaylistTrack>> GetPlaylistItems(string playlistId, string market,  int limit, int offset)
    {
        var result = new List<PlaylistTrack>();

        var spotifyClient = CreateSpotifyClient();
        if (spotifyClient != null)
        {
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
        }

        return result;
    }

    public async Task<List<PlaylistTrack>> GetPlaylistItemsAll(string playlistId, string market)
    {
        var result = new List<PlaylistTrack>();

        var spotifyClient = CreateSpotifyClient();

        if (spotifyClient != null)
        {
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
        }

        //Set the tracks position in the playlist
        for (var i = 0; i < result.Count; i++)
        {
            result[i].Position = i;
        }

        return result;
    }

    public async Task<string> RemovePlaylistItems(string playlistId, PlaylistRemoveItemsRequest request)
    {
        string snapshotId = String.Empty;

        var spotifyClient = CreateSpotifyClient();

        if (spotifyClient != null)
        {

            var result = await spotifyClient.Playlists.RemoveItems(playlistId, request);
            if (result != null)
            {
                snapshotId = result.SnapshotId;
            }
        }

        return snapshotId;
    }

    public async Task<User> GetCurrentUser()
    {
        var result = new User();

        var spotifyClient = CreateSpotifyClient();

        if (spotifyClient != null)
        {
            var currentUser = await spotifyClient.UserProfile.Current();
            result = _mapper.Map<User>(currentUser);
        }

        return result;
    }

    public string GetAuthorizationUri()
    {
        var loginRequest = new LoginRequest(
          new Uri(_authSettings.Value.CallbackUrl),
          _authSettings.Value.ClientId,
          LoginRequest.ResponseType.Code
        )
        {
            Scope = new[] 
            { 
                Scopes.UserReadPrivate,
                Scopes.UserReadEmail,
                Scopes.UserLibraryRead,
                Scopes.PlaylistReadPrivate,
                Scopes.PlaylistModifyPrivate,
                Scopes.PlaylistModifyPublic
            }
        };
        var uri = loginRequest.ToUri().ToString();

        return uri;
    }

    public async Task GetCallback(string code)
    {
        var response = await new OAuthClient().RequestToken(
            new AuthorizationCodeTokenRequest(_authSettings.Value.ClientId, _authSettings.Value.ClientSecret, code, new Uri(_authSettings.Value.CallbackUrl)));
        setAuthValues(response.AccessToken, response.RefreshToken, response.CreatedAt, response.ExpiresIn);
    }

    private void setAuthValues(string accessToken, string refreshToken, DateTime createdAt, int expiresIn)
    {
        _sessionService.SetAccessToken(accessToken);
        _sessionService.SetRefreshToken(refreshToken);
        _sessionService.SetCreatedAt(createdAt.Ticks.ToString());
        _sessionService.SetExpiresIn(expiresIn.ToString());
    }
    private bool isValidAccessToken()
    {
        long createdAtTicks = Convert.ToInt64(_sessionService.GetCreatedAt());
        DateTime createdAt = new DateTime(createdAtTicks);
        int expiresIn = Convert.ToInt32(_sessionService.GetExpiresIn());

        return DateTime.UtcNow < createdAt.AddSeconds(expiresIn);
    }

    private async void RefreshAccessToken()
    {
        string refreshToken = _sessionService.GetRefreshToken();
        string clientId = _authSettings.Value.ClientId;
        string clientSecret = _authSettings.Value.ClientSecret;

        if (String.IsNullOrWhiteSpace(refreshToken) == false
            && String.IsNullOrWhiteSpace(clientId) == false
            && !String.IsNullOrWhiteSpace(clientSecret) == false)
        {
            var response = await new OAuthClient().RequestToken(
              new AuthorizationCodeRefreshRequest(clientId, clientSecret, refreshToken)
            );

            setAuthValues(response.AccessToken, response.RefreshToken, response.CreatedAt, response.ExpiresIn);
        }
    }
}
