using System.ComponentModel;

namespace Tavily.McpServer.ClientSdk.Models.Search;

public enum SearchDepth
{
    Basic,
    Advanced
}

public enum SearchTopic
{
    General,
    News
}

public enum TimeRange
{
    Day,
    Week,
    Month,
    Year,
    D,
    W,
    M,
    Y
}

public static class TimeRangeOptions
{
    public const string Day = "day";
    public const string Week = "week";
    public const string Month = "month";
    public const string Year = "year";
    public const string D = "d";
    public const string W = "w";
    public const string M = "m";
    public const string Y = "y";
}

public record Request
{
    public Request()
    {
            
        if (!string.IsNullOrEmpty(Country))
        {
            Topic = SearchTopic.General;
        }
    }
    [Description("Search query")]
    public required string Query { get; init; }

    [Description("The depth of the search. It can be 'Basic' or 'Advanced'")]
    public SearchDepth  SearchDepth { get; init; } = SearchDepth.Basic;

    [Description("Whether to include an LLM-generated answer to the provided query. ")]
    public bool IncludeAnswer { get; init; } = false;

    [Description("The category of the search. This will determine which of our agents will be used for the search.It can be 'General' or 'News'")]
    public SearchTopic Topic { get; init; } = SearchTopic.General;

    [Description("The number of days back from the current date to include in the search results. This specifies the time frame of data to be retrieved. Please note that this feature is only available when using the 'News' search topic")]
    public int Days { get; init; } = 3;
    [Description("The time range back from the current date to include in the search results. This feature is available for both 'General' and 'News' search topics")]
    public TimeRange? TimeRange { get; init; }

    [Description("Will return all results after the specified start date. Required to be written in the format YYYY-MM-DD.")]
    public string? StartDate { get; init; } = string.Empty;
    [Description("Will return all results before the specified end date. Required to be written in the format YYYY-MM-DD")]
    public string? EndDate { get; init; } = string.Empty;

    [Description("The maximum number of search results to return, It can be maximum 20 and minimum 5")]
    public int MaxResults { get; init; } = 10;

    [Description("Include a list of query-related images in the response")]
    public bool IncludeImages { get; init; } = false;
    [Description("Include a list of query-related images and their descriptions in the response")]
    public bool IncludeImageDescriptions { get; init; } = false;
    [Description("Include the cleaned and parsed HTML content of each search result")]
    public bool IncludeRawContent { get; init; } = false;

    [Description("A list of domains to specifically include in the search results, if the user asks to search on specific sites set this to the domain of the site")]
    public List<string> IncludeDomains { get; init; } = [];
    [Description("List of domains to specifically exclude, if the user asks to exclude a domain set this to the domain of the site")]
    public List<string> ExcludeDomains { get; init; } = [];

    [Description("Boost search results from a specific country.This will prioritize content from the selected country in the search results.Available only if topic is general.Country names MUST be written in lowercase, plain English, with spaces and no underscores.")]
    public string? Country { get; init; }
    [Description("Whether to include the favicon URL for each result")]
    public bool IncludeFavicon { get; init; } = false;
}
