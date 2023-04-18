using MassTransit;
using MongoDB.Driver;
using NewsKraken.Database;
using NewsKraken.Database.Models;
using RabbitRequestModels.NewsAPI.Awnsers;
using RabbitRequestModels.NewsAPI.Requests;

namespace Core.Consumer;

public class LoadArticleConsumer : IConsumer<LoadArticleModel>
{
    private readonly NewsKrakenDBContext _dbContext;
    private readonly ArticleMapper _articleMapper;

    public LoadArticleConsumer(NewsKrakenDBContext dbContext, ArticleMapper articleMapper)
    {
        _dbContext = dbContext;
        _articleMapper = articleMapper;
    }

    public async Task Consume(ConsumeContext<LoadArticleModel> context)
    {
        var message = context.Message;
        var filter = Builders<Article>.Filter.Eq(x => x.Id, message.ArticleId);
        var article = await _dbContext.Articles.Find(filter).FirstAsync();
        var mappedArticle = _articleMapper.MapDBArticle(article);
        await context.RespondAsync(mappedArticle);
    }
}