using Core.NewsAPI;
using Core.NewsAPI.RequestModels;
using Microsoft.AspNetCore.Mvc;
using NewsKraken.Database;

namespace NewsKraken.API.Controllers;

[Controller, Route("controller")]
public class NewsAPIController : ControllerBase
{
    private readonly NewsGatherer _newsGatherer;
    private readonly NewsKrakenDBContext _dbContext;
    public NewsAPIController(NewsGatherer newsGatherer, NewsKrakenDBContext dbContext)
    {
        _newsGatherer = newsGatherer;
        _dbContext = dbContext;
    }

    [HttpGet("/news-api")]
    public async Task<IActionResult> GetNewsFiltered([FromBody] SearchNewsModel model)
    {
        var result = await _newsGatherer.GatherNews(model);

        return Ok(new NewsApiPayload()
        {
            Articles = result.Articles
        });
    }

    [HttpGet("/test")]
    public async Task<IActionResult> TestRequest()
    {
        var result = await _newsGatherer.GatherNews(new SearchNewsModel()
        {
            Q = "bitcoin"
        });

        return Ok(result);
    }

    // [HttpPost("/news.api/save")]
    // public async Task<IActionResult> SaveArticle([FromBody] SaveArticleModel model)
    // {
    //     
    // }
}