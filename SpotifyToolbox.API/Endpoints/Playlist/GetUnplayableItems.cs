using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SpotifyToolbox.API.Services.Playlist;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class GetUnplayableItems : EndpointBaseAsync
    .WithRequest<UnplayableItemsRequest>
    .WithActionResult<UnplayableItemsResponse>
{
    private readonly IGetUnplayableItems _getUnplayableItemsService;
    public GetUnplayableItems(IGetUnplayableItems getUnplayableItemsService)
    {
        _getUnplayableItemsService = getUnplayableItemsService;
    }

    [HttpGet("api/unplayablePlaylistItems")]
    public async override Task<ActionResult<UnplayableItemsResponse>> HandleAsync(
        [FromQuery] UnplayableItemsRequest request, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (request.Authorization == null)
            {
                return BadRequest(nameof(request.Authorization));
            }
            if (String.IsNullOrWhiteSpace(request.PlaylistId))
            {
                return BadRequest(nameof(request.PlaylistId));
            }

            var playlistItems = await _getUnplayableItemsService.GetPlaylistUnplayableItems(request.Authorization, request.PlaylistId);
            var response = new UnplayableItemsResponse(playlistItems);
            return Ok(response);
        } catch (Exception ex)
        {
            Log.Error("An error has occurred: {@ex}", ex);
            return Problem(ex.Message);
        }
    }
}
