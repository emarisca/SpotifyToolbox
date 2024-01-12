namespace SpotifyToolbox.API.Services
{
    public interface ISessionService
    {
        string GetAccessToken();
        string GetRefreshToken();
        string GetCreatedAt();
        string GetExpiresIn();
        void SetAccessToken(string accessToken);
        void SetRefreshToken(string refreshToken);
        void SetCreatedAt(string createdAt);
        void SetExpiresIn(string expiresIn);

    }
}