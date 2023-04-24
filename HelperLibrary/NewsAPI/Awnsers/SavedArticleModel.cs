namespace RabbitRequestModels.NewsAPI.Awnsers;

public class SavedArticleModel
{
    public Guid ArticleId { get; set; }
    public NewsAPIArticle Article { get; set; }
}