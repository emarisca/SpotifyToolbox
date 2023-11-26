using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SpotifyToolbox.API.Lib;

namespace SpotifyToolbox.API.Endpoints.Auth;

public class GetCallback : EndpointBaseAsync
    .WithRequest<string>
    .WithoutResult
{
    private readonly ISpotifyClientWrapper _spotifyClientWrapper;
    public GetCallback(ISpotifyClientWrapper spotifyClientWrapper)
    {
        _spotifyClientWrapper = spotifyClientWrapper;
    }

    [HttpGet("api/auth/getCallback")]
    public async override Task HandleAsync(
        [FromQuery] string request, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _spotifyClientWrapper.GetCallback(request);
        } catch (Exception ex)
        {
            Log.Error("An error has occurred: {@ex}", ex);
        }
    }
}
