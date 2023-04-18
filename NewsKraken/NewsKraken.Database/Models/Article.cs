using MongoDB.Bson.Serialization.Attributes;

namespace NewsKraken.Database.Models;

public class Article
{
    [BsonId]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public NewsAPISource? Source { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public string UlrToImage { get; set; }
    public string PuplishedAt { get; set; }
    public string content { get; set; }   
}

public class NewsAPISource
{
    public string Id { get; set; }
    public string Name { get; set; }
}