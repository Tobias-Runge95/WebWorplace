using RabbitRequestModels.NewsAPI.Awnsers;

namespace RabbitRequestModels.NewsAPI.Requests;

public class SaveArticleModel
{
    public Guid UserId { get; set; }
    public NewsAPIArticle Article { get; set; }
}