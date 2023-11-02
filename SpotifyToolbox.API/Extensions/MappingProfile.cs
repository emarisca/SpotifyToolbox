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
