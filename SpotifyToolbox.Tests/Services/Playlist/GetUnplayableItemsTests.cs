using Autofac.Extras.Moq;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Models;
using SpotifyToolbox.API.Services.Playlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyToolbox.Tests.Services.Playlist;

public class GetUnplayableItemsTests
{
    [Fact]
    public async void GetUnplayableItems_ShouldFilterItems()
    {
        string playlistId = "";
        string market = "MX";

        using(var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISpotifyClientWrapper>()
                .Setup(x => x.GetPlaylistItemsAll(playlistId, market))
                .Returns(Task.FromResult(GetPlaylistItemsData()));

            var getUnplayableItemsService = mock.Create<GetUnplayableItems>();
            var expected = GetUnplayableItemsData();
            var actual = await getUnplayableItemsService.GetPlaylistUnplayableItems(playlistId, market);

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
        }
    }

    private List<PlaylistTrack> GetPlaylistItemsData()
    {
        var result = new List<PlaylistTrack>
        {
            new PlaylistTrack
            {
                AddedAt = DateTime.Parse("2016-11-13T20:48:11Z"),
                Track = new Track
                {
                    Id = "0kHXwmkGd6IlCNJH6cGmWw",
                    Name = "Sugar",
                    DurationMs = 153800,
                    IsPlayable = true,
                    Uri = "spotify:track:0kHXwmkGd6IlCNJH6cGmWw",
                    Album = new Album
                    {
                        Id = "3kQs7B3H8jmWbzNecTrn1i",
                        Name = "System Of A Down"
                    },
                    Artists = new List<Artist>
                    {
                        new Artist
                        {
                            Id = "5eAWCfyUhZtHHtBdNk56l1",
                            Name = "System Of A Down"
                        }
                    }
                }
            },
            new PlaylistTrack
            {
                AddedAt = DateTime.Parse("2016-12-13T20:48:11Z"),
                Track = new Track
                {
                    Id = "0kHXwmkGd6IlCNJH6cGmWw",
                    Name = "Sugar",
                    DurationMs = 153800,
                    IsPlayable = true,
                    Uri = "spotify:track:0kHXwmkGd6IlCNJH6cGmWw",
                    Album = new Album
                    {
                        Id = "3kQs7B3H8jmWbzNecTrn1i",
                        Name = "System Of A Down"
                    },
                    Artists = new List<Artist>
                    {
                        new Artist
                        {
                            Id = "5eAWCfyUhZtHHtBdNk56l1",
                            Name = "System Of A Down"
                        }
                    }
                }
            },
            new PlaylistTrack
            {
                AddedAt = DateTime.Parse("2016-11-13T20:48:11Z"),
                Track = new Track
                {
                    Id = "5bSASu4W0HJx6CuG8rbRcA",
                    Name = "Reel Around the Fountain - 2011 Remaster",
                    DurationMs = 359226,
                    IsPlayable = true,
                    Uri = "spotify:track:5bSASu4W0HJx6CuG8rbRcA",
                    Album = new Album
                    {
                        Id = "4YD26sRXWliTvOf4WSWHz3",
                        Name = "The Smiths"
                    },
                    Artists = new List<Artist>
                    {
                        new Artist
                        {
                            Id = "3yY2gUcIsjMr8hjo51PoJ8",
                            Name = "The Smiths"
                        }
                    }
                }
            },
            new PlaylistTrack
            {
                AddedAt = DateTime.Parse("2016-11-13T20:48:11Z"),
                Track = new Track
                {
                    Id = "7CeRdUMD41PaOjaAytJLzw",
                    Name = "I Miss You",
                    DurationMs = 348205,
                    IsPlayable = false,
                    Uri = "spotify:track:7CeRdUMD41PaOjaAytJLzw",
                    Album = new Album
                    {
                        Id = "0vT3XBE3jdXc2uDmutViig",
                        Name = "25"
                    },
                    Artists = new List<Artist>
                    {
                        new Artist
                        {
                            Id = "4dpARuHxo51G3z768sgnrY",
                            Name = "Adele"
                        }
                    }
                }
            },
            new PlaylistTrack
            {
                AddedAt = DateTime.Parse("2016-11-13T20:48:11Z"),
                Track = new Track
                {
                    Id = "6uNRiuAeS3w8RLnCgBEsOj",
                    Name = "Hold on, We're Going Home",
                    DurationMs = 199573,
                    IsPlayable = false,
                    Uri = "spotify:track:6uNRiuAeS3w8RLnCgBEsOj",
                    Album = new Album
                    {
                        Id = "6UQxtMOABUsj5ti54LQdoZ",
                        Name = "BBC Radio 1's Live Lounge 2013"
                    },
                    Artists = new List<Artist>
                    {
                        new Artist
                        {
                            Id = "7Ln80lUS6He07XvHI8qqHH",
                            Name = "Arctic Monkeys"
                        }
                    }
                }
            }
        };

        return result;
    }

    private List<PlaylistTrack> GetUnplayableItemsData()
    {
        var result = new List<PlaylistTrack>
        {
            new PlaylistTrack
            {
                AddedAt = DateTime.Parse("2016-11-13T20:48:11Z"),
                Track = new Track
                {
                    Id = "7CeRdUMD41PaOjaAytJLzw",
                    Name = "I Miss You",
                    DurationMs = 348205,
                    IsPlayable = false,
                    Uri = "spotify:track:7CeRdUMD41PaOjaAytJLzw",
                    Album = new Album
                    {
                        Id = "0vT3XBE3jdXc2uDmutViig",
                        Name = "25"
                    },
                    Artists = new List<Artist>
                    {
                        new Artist
                        {
                            Id = "4dpARuHxo51G3z768sgnrY",
                            Name = "Adele"
                        }
                    }
                }
            },
            new PlaylistTrack
            {
                AddedAt = DateTime.Parse("2016-11-13T20:48:11Z"),
                Track = new Track
                {
                    Id = "6uNRiuAeS3w8RLnCgBEsOj",
                    Name = "Hold on, We're Going Home",
                    DurationMs = 199573,
                    IsPlayable = false,
                    Uri = "spotify:track:6uNRiuAeS3w8RLnCgBEsOj",
                    Album = new Album
                    {
                        Id = "6UQxtMOABUsj5ti54LQdoZ",
                        Name = "BBC Radio 1's Live Lounge 2013"
                    },
                    Artists = new List<Artist>
                    {
                        new Artist
                        {
                            Id = "7Ln80lUS6He07XvHI8qqHH",
                            Name = "Arctic Monkeys"
                        }
                    }
                }
            }
        };

        return result;
    }
}
