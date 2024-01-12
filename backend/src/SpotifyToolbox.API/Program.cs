using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Services.Playlist;
using SpotifyToolbox.API.Startup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyAPI.Web;
using static SpotifyAPI.Web.Scopes;
using Microsoft.OpenApi.Models;
using SpotifyToolbox.API.Services;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper();

        builder.Services.AddTransient<ISpotifyClientWrapper, SpotifyClientWrapper>();
        builder.Services.AddTransient<IGetDuplicateItems, GetDuplicateItems>();
        builder.Services.AddTransient<IGetUnplayableItems, GetUnplayableItems>();

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddSingleton<ISessionService, SessionService>();

        builder.Host.UseSerilog((context, configuration) => 
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddCors(o => o.AddPolicy("_myAllowedSpecificOrigins", builder =>
        {
            builder.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));

        // Configure session.
        builder.Services.AddMemoryCache();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(10);
            options.Cookie.Name = ".SpotifyToolbox.Session";
        });

        builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("Spotify:Auth"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();
        app.UseCors("_myAllowedSpecificOrigins");
        app.UseAuthorization();
        app.UseSession();
        app.MapControllers();

        app.Run();
    }
}