using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Services.Playlist;

namespace SpotifyToolbox.API.Endpoints.Auth
{
    public class GetAuthorizationUri : EndpointBase
    {
        private readonly ISpotifyClientWrapper _spotifyClientWrapper;
        public GetAuthorizationUri(ISpotifyClientWrapper spotifyClientWrapper)
        {
            _spotifyClientWrapper = spotifyClientWrapper;
        }
 
        [HttpGet("api/auth/getAuthorizationUri")]
        public string Handle()
        {
            return _spotifyClientWrapper.GetAuthorizationUri();
        }
    }
}
