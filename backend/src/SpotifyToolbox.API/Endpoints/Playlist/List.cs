using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SpotifyAPI.Web;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Models;
using SpotifyToolbox.API.Services.Playlist;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class List : EndpointBaseAsync
    .WithRequest<PlaylistRequest>
    .WithActionResult<ResponseModel<Models.Playlist>>
{
    private readonly IListPlaylists _listPlaylistsService;
    public List(IListPlaylists listPlaylistsService)
    {
        _listPlaylistsService = listPlaylistsService;
    }

    [HttpGet("api/playlist")]
    public async override Task<ActionResult<ResponseModel<Models.Playlist>>> HandleAsync(
        [FromQuery] PlaylistRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (request.Limit == 0 || request.Limit > 50)
            {
                request.Limit = 50;
            }

            var response = await _listPlaylistsService.GetUserPlaylists(request.Limit, request.Offset);
           
            return Ok(response);
        } catch (Exception ex)
        {
            Log.Error("An error has occurred: {@ex}", ex);
            return BadRequest(ex.Message);
        }
    }
}
