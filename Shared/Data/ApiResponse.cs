using System.Text.Json.Serialization;

namespace Shared.Data
{
    public class ApiResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("result")]
        public object? Result { get; set; }

        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }

        public override string ToString()
        {
            return $"{nameof(Success)}: {Success}, {(Success ? $"{nameof(Result)}: ({Result})" : $"{nameof(ErrorMessage)}: ({ErrorMessage})")}";
        }
    }
}
