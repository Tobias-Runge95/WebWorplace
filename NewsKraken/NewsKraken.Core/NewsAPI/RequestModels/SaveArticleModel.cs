namespace Core.NewsAPI.RequestModels;

public class SaveArticleModel
{
    public Guid UserId { get; set; }
    public NewsAPIArticle Article { get; set; }
}