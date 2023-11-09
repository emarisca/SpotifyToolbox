using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class TracksResponse : IResponse<PlaylistTrack>
{
    public IEnumerable<Models.PlaylistTrack> Data { get; }

    public TracksResponse(IEnumerable<Models.PlaylistTrack> data)
    {
        Data = data;
    }
}
