using MassTransit;
using NewsKraken.Database;
using NewsKraken.Database.Models;
using RabbitRequestModels.NewsAPI.Requests;

namespace Core.Consumer;

public class SaveNewsConsumer : IConsumer<SaveArticleModel>
{
    private readonly NewsKrakenDBContext _dbContext;
    private readonly ArticleMapper _articleMapper;

    public SaveNewsConsumer(NewsKrakenDBContext dbContext, ArticleMapper articleMapper)
    {
        _dbContext = dbContext;
        _articleMapper = articleMapper;
    }

    public async Task Consume(ConsumeContext<SaveArticleModel> context)
    {
        var message = context.Message;
        var article = _articleMapper.MapArticle(message);
        await _dbContext.Articles.InsertOneAsync(article);
    }
}