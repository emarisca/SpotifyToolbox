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
            if (String.IsNullOrWhiteSpace(request.PlaylistId))
            {
                return BadRequest($"Field {nameof(request.PlaylistId)} is required.");
            }
            if (String.IsNullOrWhiteSpace(request.Market))
            {
                return BadRequest($"Field {nameof(request.Market)} is required.");
            }

            var playlistItems = await _getUnplayableItemsService.GetPlaylistUnplayableItems(request.PlaylistId, request.Market);
            var response = new UnplayableItemsResponse(playlistItems);
            return Ok(response);
        } catch (Exception ex)
        {
            Log.Error("An error has occurred: {@ex}", ex);
            return Problem(ex.Message);
        }
    }
}
