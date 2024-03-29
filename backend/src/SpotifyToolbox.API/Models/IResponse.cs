﻿namespace SpotifyToolbox.API.Models;

public interface IResponse<T>
{
    IEnumerable<T> Data { get; }
    int? Total { get; set; }
}
