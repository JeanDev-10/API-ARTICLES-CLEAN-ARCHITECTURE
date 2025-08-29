
using Articles.Domain.Exceptions;

namespace Articles.Domain.ValueObjects.Articles;

public sealed class ArticleName
{
    public string Value { get; }
    public ArticleName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("El nombre del artículo es requerido.");

        if (value.Length > 100)
            throw new DomainException("El nombre no puede exceder 100 caracteres.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
