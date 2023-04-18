﻿using RabbitRequestModels;
using RabbitRequestModels.NewsAPI.Awnsers;

namespace Core.NewsAPI.RequestModels;

public class SaveMultipleArticlesModel
{
    public Guid UserId { get; set; }
    public List<NewsAPIArticle> Articles { get; set; }
}