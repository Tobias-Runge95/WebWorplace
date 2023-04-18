using Core.NewsAPI;
using MassTransit;
using RabbitRequestModels;
using RabbitRequestModels.NewsAPI.Requests;

namespace Core.Consumer;

public class GetNews : IConsumer<SearchNewsModel>
{
    private readonly NewsGatherer _newsGatherer;

    public GetNews(NewsGatherer newsGatherer)
    {
        _newsGatherer = newsGatherer;
    }

    public async Task Consume(ConsumeContext<SearchNewsModel> context)
    {
        var snm = context.Message;
        var news = await _newsGatherer.GatherNews(snm);

        await context.RespondAsync(new NewsAPIResponseModel()
        {
            Articles = news.Articles,
            Status = news.Status,
            TotalResults = news.TotalResults
        });
    }
}