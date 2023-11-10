namespace SpotifyToolbox.API.Extensions;

public static class ListExtensions
{
    public static string Stringify(this IEnumerable<string> values, string separator)
    {
        return string.Join(separator, values);
    }
}
