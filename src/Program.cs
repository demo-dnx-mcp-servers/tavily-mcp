using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tavily.McpServer.ClientSdk;
using Tavily.McpServer.ClientSdk.Configuration;
using Tavily.McpServer.Tools;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services.AddOptions<TavilyClientOptions>()
            .Bind(builder.Configuration.GetSection(TavilyClientOptions.TavilyClientOptionName))
            .ValidateDataAnnotations();
builder.Services.AddHttpClient<TavilyClient>((sp, client) =>
{
    var tavilyConfig = sp.GetRequiredService<IOptions<TavilyClientOptions>>();
    client.BaseAddress = new Uri(tavilyConfig.Value.BaseAddress);
    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"Bearer {tavilyConfig.Value.ApiKey}");
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<TavilyTools>();

await builder.Build().RunAsync();
