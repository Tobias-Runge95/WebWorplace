using RabbitRequestModels.NewsAPI.Awnsers;

namespace Core.NewsAPI;

public class NewsApiPayload
{
    public List<NewsAPIArticle> Articles { get; set; }
}