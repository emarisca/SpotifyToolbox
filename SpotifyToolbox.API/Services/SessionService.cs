using SpotifyAPI.Web.Http;

namespace SpotifyToolbox.API.Services
{
    public class SessionService : ISessionService
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private string ACCESS_TOKEN_KEY = "accessToken";
        private string REFRESH_TOKEN_KEY = "refreshToken";
        private string TOKEN_CREATED_AT_KEY = "createdAt";
        private string TOKEN_EXPIRES_IN_KEY = "expiresIn";

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetValue(string key)
        {
            string value = null!;
            if (_httpContextAccessor.HttpContext.Session.TryGetValue(key, out var valueBytes))
            {
                value = System.Text.Encoding.UTF8.GetString(valueBytes);
            }

            return value!;
        }

        public void SetAccessToken(string accessToken)
        {
            _httpContextAccessor.HttpContext.Session.SetString(ACCESS_TOKEN_KEY, accessToken);
        }

        public void SetRefreshToken(string refreshToken)
        {
            _httpContextAccessor.HttpContext.Session.SetString(REFRESH_TOKEN_KEY, refreshToken);
        }

        public void SetCreatedAt(string createdAt)
        {
            _httpContextAccessor.HttpContext.Session.SetString(TOKEN_CREATED_AT_KEY, createdAt);
        }

        public void SetExpiresIn(string expiresIn)
        {
            _httpContextAccessor.HttpContext.Session.SetString(TOKEN_EXPIRES_IN_KEY, expiresIn);
        }

        public string GetAccessToken()
        {
            return GetValue(ACCESS_TOKEN_KEY);
        }

        public string GetRefreshToken()
        {
            return GetValue(REFRESH_TOKEN_KEY);
        }

        public string GetCreatedAt()
        {
            return GetValue(TOKEN_CREATED_AT_KEY);
        }

        public string GetExpiresIn()
        {
            return GetValue(TOKEN_EXPIRES_IN_KEY);
        }
    }
}
