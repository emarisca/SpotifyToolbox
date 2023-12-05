using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Moq;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Models;
using SpotifyToolbox.API.Services.Playlist;

namespace SpotifyToolbox.Tests.Services.Playlist;

public class GetDuplicateItemsTests
{
    [Fact]
    public async void GetPlaylistDuplicateItems_ShouldAggregateDupes()
    {
        string playlistId = "";
        string market = "MX";

        using (var mock = AutoMock.GetLoose())
        {
            mock.Mock<ISpotifyClientWrapper>()
                .Setup(x => x.GetPlaylistItemsAll(playlistId, market))
                .Returns(Task.FromResult(GetPlaylistItemsData()));

            var getDuplicateItemsService = mock.Create<GetDuplicateItems>();
            var expected = GetGroupedDuplicateItemsData();
            var actual = await getDuplicateItemsService.GetPlaylistDuplicateItems(playlistId, market);

            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);
            Assert.Equal(expected[0].Tracks.Count, actual[0].Tracks.Count);
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
            }
        };

        return result;
    }

    private List<DuplicatePlaylistItem> GetGroupedDuplicateItemsData()
    {
        var result = new List<DuplicatePlaylistItem>
        {
            new DuplicatePlaylistItem
            {
                DuplicateName = "Sugar",
                Tracks = new List<PlaylistTrack>
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
                    }
                }
            }
        };

        return result;
    }
}
