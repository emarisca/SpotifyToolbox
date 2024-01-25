using AutoMapper;
using SpotifyToolbox.API.Lib;
using SpotifyToolbox.API.Models;

namespace SpotifyToolbox.API.Services.Playlist
{
    public class ListPlaylistsService : IListPlaylists
    {
        private readonly ISpotifyClientWrapper _spotifyClientWrapper;
        private IMapper _mapper;

        public ListPlaylistsService(ISpotifyClientWrapper spotifyClientWrapper, IMapper mapper)
        {
            _spotifyClientWrapper = spotifyClientWrapper;
            _mapper = mapper;
        }

        public async Task<ResponseModel<Models.Playlist>> GetUserPlaylists(int limit, int offset)
        {
            var result = await _spotifyClientWrapper.GetUserPlaylists(limit, offset);
            List<Models.Playlist> playlists = new List<Models.Playlist>();
            int? total = 0;
            if (result != null && result.Items != null)
            {
                playlists = result.Items.Select(x => _mapper.Map<Models.Playlist>(x)).ToList();
                total = result.Total;
            }

            var response = new ResponseModel<Models.Playlist>(playlists);
            response.Total = total;

            return response;
        }
    }
}
