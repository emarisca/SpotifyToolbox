﻿using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Endpoints.Playlist;

public class PlaylistsResponse : IResponse<Models.Playlist>
{
    public IEnumerable<Models.Playlist> Data { get; }
    public int? Total { get; set; }

    public PlaylistsResponse(IEnumerable<Models.Playlist> data)
    {
        Data = data;
    }
}
