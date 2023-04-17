using MongoDB.Driver;
using NewsKraken.Database.Models;

namespace NewsKraken.Database;

public class NewsKrakenDBContext
{
    public NewsKrakenDBContext(string connectionString)
    {
        var mongoClient = new MongoClient(connectionString);
        var db = mongoClient.GetDatabase("NewsApi");

        Articles = db.GetCollection<Article>("Articles");
    }
    
    public IMongoCollection<Article> Articles { get; }
}