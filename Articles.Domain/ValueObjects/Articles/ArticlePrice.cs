using System;
using Articles.Domain.Exceptions;

namespace Articles.Domain.ValueObjects.Articles;

public sealed class ArticlePrice
{
    public decimal Value { get; }

    public ArticlePrice(decimal value)
    {
        if (value <= 0)
            throw new DomainException("El precio debe ser mayor a 0.");

        Value = value;
    }

    public override string ToString() => Value.ToString("F2");
}
