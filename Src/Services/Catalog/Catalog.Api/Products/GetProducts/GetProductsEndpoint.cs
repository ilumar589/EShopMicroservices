using System.Collections.Immutable;

namespace Catalog.Api.Products.GetProducts;

public sealed record GetProductResponse
{
    public IReadOnlyList<Product> Products { get; init; } = ImmutableList<Product>.Empty;
}

public sealed class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());

            var response = result.Adapt<GetProductResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}