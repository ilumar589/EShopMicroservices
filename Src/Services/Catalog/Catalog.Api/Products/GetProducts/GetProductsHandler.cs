using System.Collections.Immutable;

namespace Catalog.Api.Products.GetProducts;

public sealed record GetProductsQuery : IQuery<GetProductResult>;

public sealed record GetProductResult
{
    public IReadOnlyList<Product> Products { get; init; } = ImmutableList<Product>.Empty;
}

internal sealed class GetProductsQueryHandler(
    IDocumentSession session,
    ILogger<GetProductsQueryHandler> logger
) : IQueryHandler<GetProductsQuery, GetProductResult>
{
    private readonly IDocumentSession _session = session ?? throw new ArgumentNullException(nameof(session));
    private readonly ILogger<GetProductsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductsQueryHandler. Handle called with {@Query}", query);

        var products = await _session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductResult { Products = products };
    }
}