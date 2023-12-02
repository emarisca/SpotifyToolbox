using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SpotifyToolbox.API.Lib;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class GetItems : EndpointBaseAsync
    .WithRequest<ItemsRequest>
    .WithActionResult<ItemsResponse>
{
    private readonly ISpotifyClientWrapper _spotifyClientWrapper;
    public GetItems(ISpotifyClientWrapper spotifyClientWrapper)
    {
        _spotifyClientWrapper = spotifyClientWrapper;
    }

    [HttpGet("api/playlistItems")]
    public async override Task<ActionResult<ItemsResponse>> HandleAsync(
        [FromQuery] ItemsRequest request, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(request.PlaylistId))
            {
                return BadRequest(nameof(request.PlaylistId));
            }
            if (request.Limit == 0 || request.Limit > 100)
            {
                request.Limit = 100;
            }

            var user = await _spotifyClientWrapper.GetCurrentUser();

            var playlistItems = await _spotifyClientWrapper.GetPlaylistItems(request.PlaylistId, user.Country, request.Limit, request.Offset);
            var response = new ItemsResponse(playlistItems);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Log.Error("An error has occurred: {@ex}", ex);
            return Problem(ex.Message);
        }
    }
}
