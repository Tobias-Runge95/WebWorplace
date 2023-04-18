using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitRequestModels;
using RabbitRequestModels.NewsAPI.Awnsers;

namespace WebWorkPlace.API.Controllers;

[ApiController, Route("controller")]
public class AuthenticationController : ControllerBase
{
    private readonly IBus _bus;

    public AuthenticationController(IBus bus)
    {
        _bus = bus;
    }

    [HttpGet("/test")]
    public async Task<IActionResult> Login()
    {
        // Uri uri = new Uri("rabbitmq://localhost/ABC");
        // var endpoint = await _bus.GetSendEndpoint(uri);
        // await endpoint.Send(new TestModel(){Message = "Test message"});
        var test = await _bus.Request<TestModel, NewsAPIResponseModel>(new TestModel() {Message = "Test message"});
        var response = test.Message;
        return Ok(response);
    }
}