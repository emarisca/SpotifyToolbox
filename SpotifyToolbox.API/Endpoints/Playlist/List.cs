using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using SpotifyToolbox.API.Lib;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class List : EndpointBaseAsync
    .WithRequest<PlaylistRequest>
    .WithActionResult<PlaylistsResponse>
{
    private readonly ISpotifyClientWrapper _spotifyClientWrapper;
    public List(ISpotifyClientWrapper spotifyClientWrapper)
    {
        _spotifyClientWrapper = spotifyClientWrapper;
    }

    [HttpGet("api/playlist")]
    public async override Task<ActionResult<PlaylistsResponse>> HandleAsync(
        [FromQuery] PlaylistRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (request.Authorization == null)
            {
                throw new ArgumentNullException (nameof (request.Authorization));
            }
            if (request.Limit == 0 || request.Limit > 50)
            {
                request.Limit = 50;
            }


            var spotifyPlaylist = await _spotifyClientWrapper.GetUserPlaylists(request.Authorization, request.Limit, request.Offset);
            var response = new PlaylistsResponse(spotifyPlaylist);

            return Ok(response);
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
