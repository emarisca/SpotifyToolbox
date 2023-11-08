using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class TracksResponse : IResponse<Track>
{
    public IEnumerable<Models.Track> Data { get; }

    public TracksResponse(IEnumerable<Models.Track> data)
    {
        Data = data;
    }
}
