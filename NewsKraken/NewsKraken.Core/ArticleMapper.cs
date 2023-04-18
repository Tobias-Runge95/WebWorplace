using Core.NewsAPI.RequestModels;
using NewsKraken.Database.Models;
using RabbitRequestModels.NewsAPI.Awnsers;
using RabbitRequestModels.NewsAPI.Requests;
using NewsAPISource = NewsKraken.Database.Models.NewsAPISource;

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

    public SavedArticleModel MapDBArticle(Article article)
    {
        return new SavedArticleModel()
        {
            Article = new NewsAPIArticle()
            {
                Author = article.Author,
                content = article.content,
                Description = article.Description,
                PuplishedAt = article.PuplishedAt,
                Source = new RabbitRequestModels.NewsAPI.Awnsers.NewsAPISource()
                {
                    Id = article.Source.Id,
                    Name = article.Source.Name
                },
                Title = article.Title,
                UlrToImage = article.UlrToImage,
                Url = article.Url,
            },
            ArticleId = article.Id
        };
    }

    public List<SavedArticleModel> MapDBArticles(List<Article> articles)
    {
        return articles.Select(x =>
        {
            return MapDBArticle(x);
        }).ToList();
    }
}