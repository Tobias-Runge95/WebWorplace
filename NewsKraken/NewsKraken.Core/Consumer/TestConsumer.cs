using Core.NewsAPI;
using Core.NewsAPI.RequestModels;
using MassTransit;
using RabbitRequestModels;
using NewsAPIResponseModel = RabbitRequestModels.NewsAPIResponseModel;

namespace Core.Consumer;

public class TestConsumer : IConsumer<TestModel>
{
    private readonly NewsGatherer _newsGatherer;

    public TestConsumer(NewsGatherer newsGatherer)
    {
        _newsGatherer = newsGatherer;
    }

    public async Task Consume(ConsumeContext<TestModel> context)
    {
        var data = context.Message;
        var result = await _newsGatherer.GatherNews(new SearchNewsModel()
        {
            Q = "bitcoin"
        });
        await context.RespondAsync<NewsAPIResponseModel>(result);
    }
}