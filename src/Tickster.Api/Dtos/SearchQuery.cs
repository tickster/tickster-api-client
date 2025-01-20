using System.Web;

namespace Tickster.Api.Dtos;

// FIXME: This relates to queries in the Event API - should be moved to a more appropriate location
public class SearchQuery
{
    public int Take { get; set; } = 10;
    public int Skip { get; set; } = 0;
    public string? Search { get; set; }
    public string? Prefix { get; set; }
    public string? Filter { get; set; }

    public string ToQueryString()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["take"] = Take.ToString();
        query["skip"] = Skip.ToString();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            query["query"] = Search;

            if (!string.IsNullOrWhiteSpace(Prefix) && !string.IsNullOrWhiteSpace(Filter))
            {
                query["query"] += $" {Prefix}:{Filter}";
            }
        }

        return query.ToString() ?? string.Empty;
    }
}
