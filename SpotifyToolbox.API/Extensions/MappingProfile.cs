﻿using AutoMapper;
using SpotifyAPI.Web;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Startup;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PrivateUser, User>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.DisplayName,
                opt => opt.MapFrom(src => src.DisplayName))
            .ForMember(
                dest => dest.Country,
                opt => opt.MapFrom(src => src.Country))
            .ForMember(
                dest => dest.Email,
                opt => opt.MapFrom(src => src.Email));

        CreateMap<FullPlaylist, Playlist>()
            .ForMember(
                dest => dest.OwnerId,
                opt => opt.MapFrom(src => src.Owner.Id))
            .ForMember(
                dest => dest.OwnerName,
                opt => opt.MapFrom(src => src.Owner.DisplayName))
            .ForMember(
                dest => dest.Size,
                opt => opt.MapFrom(src => src.Tracks.Total));

        CreateMap<PlaylistTrack<IPlayableItem>, PlaylistTrack>()
            .ForMember(
                dest => dest.AddedAt,
                prop => prop.MapFrom(src => src.AddedAt));

        CreateMap<FullTrack, Track>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.DurationMs,
                opt => opt.MapFrom(src => src.DurationMs))
            .ForMember(
                dest => dest.IsPlayable,
                opt => opt.MapFrom(src => src.IsPlayable))
            .ForMember(
                dest => dest.Uri,
                opt => opt.MapFrom(src => src.Uri));

        CreateMap<SimpleAlbum, Album>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));

        CreateMap<SimpleArtist, Artist>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));
    }
}
