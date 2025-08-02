using System.Text.Json.Serialization;
using Tavily.McpServer.ClientSdk.JsonConverter;

namespace Tavily.McpServer.ClientSdk.Models.Search;

public class Image
{
    public  string Url { get; init; } = default!;
    public string? Description { get; init; }

    // Allow implicit conversion from string
    public static implicit operator Image(string url)
    {
        return new Image { Url = url.Trim() };
    }
}

public class Result
{
    public string Title { get; init; } = default!;
    public string Url { get; init; } = default!;
    public string Content { get; init; } = default!;
    public string RawContent { get; init; } = default!;
    public string Favicon { get; init; } = default!;
}
public class Response
{
    public string Query { get; init; } = default!;
    public string Answer { get; init; } = default!;

    [JsonConverter(typeof(ImageJsonConverter))]
    public List<Image> Images { get; init; } = [];
    public List<Result> Results { get; init; } = [];
}
