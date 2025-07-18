namespace BatteriesAPI.Models
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }

        public T? Data { get; set; }

        public Error? Error { get; set; }
    }
}
