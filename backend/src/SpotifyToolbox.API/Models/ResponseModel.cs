namespace SpotifyToolbox.API.Models
{
    public class ResponseModel<T> : IResponse<T>
    {
        public IEnumerable<T> Data { get; }

        public int? Total { get; set ; }

        public ResponseModel(IEnumerable<T> data)
        {
            Data = data;
        }
    }
}
