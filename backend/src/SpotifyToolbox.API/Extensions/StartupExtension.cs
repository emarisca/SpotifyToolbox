using AutoMapper;

namespace SpotifyToolbox.API.Startup;

public static class StartupExtension
{
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        return services.AddSingleton(mapper);
    }
}
