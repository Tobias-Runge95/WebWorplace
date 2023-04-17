namespace Core.NewsAPI.RequestModels;

public class SearchNewsModel
{
    public string Q { get; set; }
    public string Sources { get; set; }
    public string Domians { get; set; }
    public string ExcludeDomians { get; set; }
    public string Language { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public NewsAPISortOptions SortBy { get; set; }
    public int PageSize { get; set; } = 5;
    public int PageNumber { get; set; }
    public NewsAPIEnpoints Endpoint { get; set; }
}

public enum NewsAPIEnpoints
{
    EveryThing, TopHeadlines
}

public enum NewsAPISortOptions
{
    relevancy, popularity, publishedAt
}