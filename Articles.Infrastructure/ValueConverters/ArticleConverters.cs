using System;
using Articles.Domain.ValueObjects.Articles;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Articles.Infrastructure.ValueConverters;

public static class ArticleConverters
{
    public static readonly ValueConverter<ArticlePrice, decimal> PriceConverter =
            new(v => v.Value, v => new ArticlePrice(v));

    public static readonly ValueConverter<ArticleName, string> NameConverter =
        new(v => v.Value, v => new ArticleName(v));
    public static readonly ValueConverter<ArticleDescription, string> DescriptionConverter =
       new(v => v.Value,
       v => new ArticleDescription(v));
}
