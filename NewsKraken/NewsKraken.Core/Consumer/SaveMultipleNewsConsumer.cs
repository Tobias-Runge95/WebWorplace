using Core.NewsAPI.RequestModels;
using MassTransit;
using NewsKraken.Database;

namespace Core.Consumer;

public class SaveMultipleNewsConsumer : IConsumer<SaveMultipleArticlesModel>
{
    private readonly NewsKrakenDBContext _dbContext;
    private readonly ArticleMapper _articleMapper;

    public SaveMultipleNewsConsumer(NewsKrakenDBContext dbContext, ArticleMapper articleMapper)
    {
        _dbContext = dbContext;
        _articleMapper = articleMapper;
    }

    public async Task Consume(ConsumeContext<SaveMultipleArticlesModel> context)
    {
        var message = context.Message;
        var mappedArticles = _articleMapper.MapArticles(message);
        await _dbContext.Articles.InsertManyAsync(mappedArticles);
    }
}