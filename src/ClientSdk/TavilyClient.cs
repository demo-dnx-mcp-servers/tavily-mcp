using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using UsageResponse = Tavily.McpServer.ClientSdk.Models.Usage.Response;
using SearchRequest = Tavily.McpServer.ClientSdk.Models.Search.Request;
using SearchResponse = Tavily.McpServer.ClientSdk.Models.Search.Response;
namespace Tavily.McpServer.ClientSdk;

public class TavilyClient(HttpClient client)
{
    private readonly JsonSerializerOptions jsonSerializer = new()
    {
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };
    public async ValueTask<UsageResponse?> GetUsageAsync(CancellationToken cancellationToken = default)
    {
        var response = await client.GetAsync("usage", cancellationToken);
        response.EnsureSuccessStatusCode();
        var responeContent =await response.Content.ReadAsStringAsync(cancellationToken);
        return await response.Content.ReadFromJsonAsync<UsageResponse>(jsonSerializer, cancellationToken);
    }

    public async Task<SearchResponse?> SearchAsync(SearchRequest request,CancellationToken cancellationToken = default)
    {
        var response = await client.PostAsJsonAsync("search", request, jsonSerializer, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SearchResponse>(jsonSerializer, cancellationToken);
    }
}
