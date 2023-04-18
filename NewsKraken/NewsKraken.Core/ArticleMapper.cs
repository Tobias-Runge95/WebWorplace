using Core.NewsAPI.RequestModels;
using NewsKraken.Database.Models;
using RabbitRequestModels.NewsAPI.Requests;

namespace Core;

public class ArticleMapper
{
    public Article MapArticle(SaveArticleModel sam)
    {
        return new Article()
        {
            Author = sam.Article.Author,
            content = sam.Article.content,
            Description = sam.Article.Description,
            Id = Guid.NewGuid(),
            PuplishedAt = sam.Article.PuplishedAt,
            Source = new NewsAPISource()
            {
                Id = sam.Article.Source.Id,
                Name = sam.Article.Source.Name
            },
            Title = sam.Article.Title,
            UlrToImage = sam.Article.UlrToImage,
            Url = sam.Article.Url,
            UserId = sam.UserId
        };
    }

    public List<Article> MapArticles(SaveMultipleArticlesModel smam)
    {
        var mappedArticles = new List<Article>();

        foreach (var sam in smam.Articles)
        {
            mappedArticles.Add(new Article()
            {
                Author = sam.Author,
                content = sam.content,
                Description = sam.Description,
                Id = Guid.NewGuid(),
                PuplishedAt = sam.PuplishedAt,
                Source = new NewsAPISource()
                {
                    Id = sam.Source.Id,
                    Name = sam.Source.Name
                },
                Title = sam.Title,
                UlrToImage = sam.UlrToImage,
                Url = sam.Url,
                UserId = smam.UserId
            });
        }

        return mappedArticles;
    }
}