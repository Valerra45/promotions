using System.Text.Json;

namespace WebApi.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; init; }

        public string? StatusDescription { get; init; }

        public string? Message { get; init; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
