using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Serilog;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Services.Playlist;

namespace SpotifyToolbox.API.Endpoints.Playlist;

[Route("/api/playlists")]
public class RemoveItems : EndpointBaseAsync
    .WithRequest<RemoveItemsRequest>
    .WithActionResult<string>
{
    private readonly ISpotifyClientWrapper _spotifyClientWrapper;
    public RemoveItems(ISpotifyClientWrapper spotifyClientWrapper)
    {
        _spotifyClientWrapper = spotifyClientWrapper;
    }
    [HttpDelete("{playlist_id}/tracks")]
    public async override Task<ActionResult<string>> HandleAsync(
        [FromRoute] RemoveItemsRequest request, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (String.IsNullOrWhiteSpace(request.PlaylistId))
            {
                return BadRequest($"Field {nameof(request.PlaylistId)} is required.");
            }
            if (request.Body.Tracks == null || request.Body.Tracks.Count() == 0)
            {
                return BadRequest($"Field {nameof(request.Body.Tracks)} is required.");
            }

            var response = await _spotifyClientWrapper.RemovePlaylistItems(request.PlaylistId, request.Body.Tracks);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Log.Error("An error has occurred: {@ex}", ex);
            return Problem(ex.Message);
        }
    }
}
