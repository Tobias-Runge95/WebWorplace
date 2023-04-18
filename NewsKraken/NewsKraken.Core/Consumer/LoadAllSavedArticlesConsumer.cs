using MassTransit;
using MongoDB.Driver;
using NewsKraken.Database;
using NewsKraken.Database.Models;
using RabbitRequestModels.NewsAPI.Requests;

namespace Core.Consumer;

public class LoadAllSavedArticlesConsumer : IConsumer<LoadAllSavedArticles>
{
    private readonly NewsKrakenDBContext _dbContext;
    private readonly ArticleMapper _articleMapper;

    public LoadAllSavedArticlesConsumer(NewsKrakenDBContext dbContext, ArticleMapper articleMapper)
    {
        _dbContext = dbContext;
        _articleMapper = articleMapper;
    }

    public async Task Consume(ConsumeContext<LoadAllSavedArticles> context)
    {
        var message = context.Message;
        var filter = Builders<Article>.Filter.Eq(x => x.UserId, message.UserId);
        var articles = await _dbContext.Articles.Find(filter).ToListAsync();

        var mappedArticles = _articleMapper.MapDBArticles(articles);
        await context.RespondAsync(mappedArticles);
    }
}