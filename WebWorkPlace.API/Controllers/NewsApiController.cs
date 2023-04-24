using Core.NewsAPI.RequestModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitRequestModels.NewsAPI.Awnsers;
using RabbitRequestModels.NewsAPI.Requests;
using WebWorkPlace.API.Authorization.Pollecies;

namespace WebWorkPlace.API.Controllers;

[Authorize(Policy = LoggedIn.Name)]
[Authorize(Policy = NewsAccess.Name)]
[Controller, Route("controller")]
public class NewsApiController : ControllerBase
{
    private readonly IBus _bus;

    public NewsApiController(IBus bus)
    {
        _bus = bus;
    }

    [HttpGet("/news")]
    public async Task<IActionResult> GetNews([FromQuery] SearchNewsModel model)
    {
        var response = await _bus.Request<SearchNewsModel, NewsAPIResponseModel>(model);
        var result = response.Message;

        return Ok(result);
    }

    [HttpGet("/news/load")]
    public async Task<IActionResult> LoadArticle([FromQuery] LoadArticleModel model)
    {
        var response = await _bus.Request<LoadArticleModel, SavedArticleModel>(model);
        return Ok(response.Message);
    }

    [HttpGet("/news/load-all")]
    public async Task<IActionResult> LoadAllArticles([FromQuery] LoadAllSavedArticles model)
    {
        var response = await _bus.Request<LoadAllSavedArticles, List<SaveArticleModel>>(model);
        return Ok(response.Message);
    }

    [HttpPost("/news/save")]
    public async Task<IActionResult> SaveArticle([FromBody] SaveArticleModel model)
    {
        Uri uri = new Uri("rabbitmq://localhost/ABC");
        var endpoint = await _bus.GetSendEndpoint(uri);
        await endpoint.Send(model);
        return Ok();
    }

    [HttpPost("/news/save-multiple")]
    public async Task<IActionResult> SaveMultipleArticles([FromBody] SaveMultipleArticlesModel model)
    {
        await _bus.Send(model);
        return Ok();
    }
}