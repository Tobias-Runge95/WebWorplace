using Core.NewsAPI.RequestModels;
using Newtonsoft.Json;
using RabbitRequestModels.NewsAPI.Awnsers;
using RabbitRequestModels.NewsAPI.Requests;
using RestSharp;

namespace Core.NewsAPI;

public class NewsGatherer
{
    public async Task<NewsAPIResponseModel> GatherNews(SearchNewsModel model)
    {
        var key = "8e0aad47a07c4a84b9d72b0fcb04815d";
        var client =
            new RestClient($"https://newsapi.org/v2/");
        string query = "";
        switch (model.Endpoint)
        {
            case NewsAPIEnpoints.EveryThing:
                query += "everything?";
                break;
            case NewsAPIEnpoints.TopHeadlines:
                query += "top-headlines?";
                break;
        }

        query += BuildQuery(model);
        query += $"&apiKey={key}";
        var request = new RestRequest(query);
        var result = await client.GetAsync(request);
        var json = JsonConvert.DeserializeObject<NewsAPIResponseModel>(result.Content);
        return json;
    }

    private string BuildQuery(SearchNewsModel model)
    {
        string query = "";
        string sortByValue = Enum.GetName(typeof(NewsAPISortOptions), model.SortBy)!;
        string from = model.From is null ? "" : model.From.Value.Date.ToString();
        string to = model.To is null ? "" : model.To.Value.Date.ToString();
        query += $"q={model.Q}";
        query += model.Endpoint == NewsAPIEnpoints.TopHeadlines
            ? $"&country={model.Language}"
            : $"&language={model.Language}";
        query += $"&domains={model.Domians}";
        query += $"&sources={model.Sources}";
        query += $"&excludeDomains={model.ExcludeDomians}";
        query += $"&sortBy={sortByValue}";
        query += $"&from={from}";
        query += $"&to={to}";
        query += $"&pageSize={model.PageSize}";
        query += $"&pageNumber={model.PageNumber}";
        return query;
    }
}