namespace Tavily.McpServer.ClientSdk.Models.Usage;

public class AccountInfo
{
    public string CurrentPlan { get; init; } = default!;
    public double? PlanUsage { get; init; }
    public double? PlanLimit { get; init; }
    public double? PaygoUsage { get; init; }
    public double? PaygoLimit { get; init; }
}
