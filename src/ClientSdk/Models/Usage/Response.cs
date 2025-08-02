namespace Tavily.McpServer.ClientSdk.Models.Usage;

public class Response
{
    public KeyInfo Key { get; init; } = default!;
    public AccountInfo Account { get; init; } = default!;
}
