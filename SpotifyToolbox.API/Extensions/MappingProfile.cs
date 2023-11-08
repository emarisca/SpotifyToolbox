using AutoMapper;
using SpotifyAPI.Web;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Startup;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SimplePlaylist, Playlist>()
            .ForMember(
                dest => dest.OwnerId,
                opt => opt.MapFrom(src => src.Owner.Id))
            .ForMember(
                dest => dest.OwnerName,
                opt => opt.MapFrom(src => src.Owner.DisplayName))
            .ForMember(
                dest => dest.Size,
                opt => opt.MapFrom(src => src.Tracks.Total));

        CreateMap<FullTrack, Track>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.DurationMs,
                opt => opt.MapFrom(src => src.DurationMs));

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
        //CreateMap<SimplePlaylist, Playlist>()
        //        .ForMember(
        //            dest => dest.OwnerId,
        //            opt => opt.MapFrom(src => src.Owner.Id))
        //        .ForMember(
        //            dest => dest.OwnerName,
        //            opt => opt.MapFrom(src => src.Owner.DisplayName))
        //        .ForMember(
        //            dest => dest.Size,
        //            opt => opt.MapFrom(src => src.Tracks.Total))
    }
}
