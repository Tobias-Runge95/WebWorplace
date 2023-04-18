using Core.NewsAPI;
using MassTransit;
using RabbitRequestModels;
using RabbitRequestModels.NewsAPI.Awnsers;
using RabbitRequestModels.NewsAPI.Requests;

namespace Core.Consumer;

public class GetNewsConsumer : IConsumer<SearchNewsModel>
{
    private readonly NewsGatherer _newsGatherer;

    public GetNewsConsumer(NewsGatherer newsGatherer)
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