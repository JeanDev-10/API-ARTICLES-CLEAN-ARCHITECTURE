using System;
using Articles.Domain.Exceptions;

namespace Articles.Domain.ValueObjects.Articles;

public sealed class ArticleDescription
{
    public string Value { get; }

    public ArticleDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("La descripción es requerida.");

        if (value.Length > 500)
            throw new DomainException("La descripción no puede exceder 500 caracteres.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
