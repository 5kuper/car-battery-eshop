namespace BatteriesAPI.Models
{
    public record ErrorDetails(string Id, string Msg);

    public class Error
    {
        public required string Code { get; set; }

        public string? Msg { get; set; }

        public List<ErrorDetails> Details { get; set; } = [];
    }
}
