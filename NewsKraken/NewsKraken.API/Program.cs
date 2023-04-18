using Core.NewsAPI;
using Core.NewsAPI.RequestModels;
using NewsKraken.API;
using NewsKraken.Database;

var builder = WebApplication.CreateBuilder(args);
var configManager = builder.Configuration;
var config = new Config();
configManager.Bind(config);
builder.Services.Configure<Config>(configManager);
// Add services to the container.
builder.Services.RegisterMassTransit();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Services.AddScoped<NewsKrakenDBContext>(x => new NewsKrakenDBContext(config.ConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
