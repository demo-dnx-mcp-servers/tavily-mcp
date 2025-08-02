using ModelContextProtocol.Server;
using System.ComponentModel;
using Tavily.McpServer.ClientSdk;
using Tavily.McpServer.ClientSdk.Models.Search;

namespace Tavily.McpServer.Tools;

public class TavilyTools(TavilyClient tavilyClient)
{
    [McpServerTool(Name = "tavily-search")]
    [Description("A powerful web search tool that provides comprehensive, real-time results using Tavily's AI search engine. Returns relevant web content with customizable parameters for result count, content type, and domain filtering. Ideal for gathering current information, news, and detailed web content analysis.")]
    public async Task<string> TavilySearch(Request searchRequest, CancellationToken ct)
    {

            var response = await tavilyClient.SearchAsync(searchRequest, ct);

            var output = new List<string>();

            if (!string.IsNullOrEmpty(response!.Answer))
            {
                output.Add($"Answer: {response.Answer}");
            }

            output.Add("Detailed Results:");
            response.Results.ForEach(result =>
            {
                output.Add($"\nTitle: {result.Title}");
                output.Add($"URL: {result.Url}");
                output.Add($"Content:{result.Content}");
                if (!string.IsNullOrEmpty(result.RawContent))
                {
                    output.Add($"Raw Content: {result.RawContent}");
                }
                if (!string.IsNullOrEmpty(result.Favicon))
                {
                    output.Add($"Favicon: {result.Favicon}");
                }
            });

            // Add images section if available
            if (response.Images.Count > 0)
            {
                int numberIndex = 0;
                output.Add("\nImages:");
                response.Images.ForEach((image) =>
                {
                    numberIndex++;
                    output.Add($"\n[{numberIndex}] URL: {image.Url}");
                    if (!string.IsNullOrEmpty(image.Description))
                    {
                        output.Add($"   Description: {image.Description}");
                    }
                });
            }
            return string.Join('\n', output);
    }

}
