using System.ComponentModel.DataAnnotations;

namespace Tavily.McpServer.ClientSdk.Configuration;

public class TavilyClientOptions
{
    public const string TavilyClientOptionName = "TavilyClientOption";

    public string BaseAddress { get; init; } = Environment.GetEnvironmentVariable("TAVILY_BASE_ADDRESS") ?? "https://api.tavily.com"!;

    [Required]
    public string ApiKey { get; init; } = Environment.GetEnvironmentVariable("TAVILY_API_KEY")  ?? throw new ArgumentException("TAVILY_API_KEY is required.");
}
