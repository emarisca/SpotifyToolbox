using SpotifyAPI.Web;
using SpotifyToolbox.API.Endpoints.Playlist;
using SpotifyToolbox.API.Lib;

namespace SpotifyToolbox.API.Services.Playlist;

public class RemovePlaylistItemsService : IRemovePlaylistItemsService
{
    private readonly ISpotifyClientWrapper _spotifyClientWrapper;
    public RemovePlaylistItemsService(ISpotifyClientWrapper spotifyClientWrapper)
    {
        _spotifyClientWrapper = spotifyClientWrapper;
    }
    public async Task<string> RemovePlaylistItems(RemoveItemsRequest request)
    {
        if (request == null || request.Body == null)
        {
            throw new Exception("Invalid request");
        }

        var itemsToRemove = new List<PlaylistRemoveItemsRequest.Item>();
        foreach(var item in request.Body.Tracks)
        {
            itemsToRemove.Add(new PlaylistRemoveItemsRequest.Item
            {
                Uri = item.Uri,
                Positions = item.Positions
            });
        }

        var playlistRemoveItemsRequest = new PlaylistRemoveItemsRequest()
        {
            Tracks = itemsToRemove
        };

        var response = await _spotifyClientWrapper.RemovePlaylistItems(request.PlaylistId, playlistRemoveItemsRequest);
        return response;
    }
}
