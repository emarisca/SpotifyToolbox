using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Services.Playlist;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class GetDuplicateItems : EndpointBaseAsync
    .WithRequest<DuplicateItemsRequest>
    .WithActionResult<DuplicateItemsResponse>
{
    private readonly IGetDuplicateItems _getDuplicateItemsService;
    public GetDuplicateItems(IGetDuplicateItems getDuplicateItemsService)
    {
        _getDuplicateItemsService = getDuplicateItemsService;
    }

    [HttpGet("/api/duplicatePlaylistItems")]
    public async override Task<ActionResult<DuplicateItemsResponse>> HandleAsync(
        [FromQuery] DuplicateItemsRequest request, 
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


            var playlistItems = await _getDuplicateItemsService.GetPlaylistDuplicateItems(request.Authorization, request.PlaylistId);
            var response = new DuplicateItemsResponse(playlistItems);
            return response;
        }
        catch (Exception ex)
        {
            Log.Error("An error has occurred: {@ex}", ex);
            return Problem(ex.Message);
        }
    }
}
