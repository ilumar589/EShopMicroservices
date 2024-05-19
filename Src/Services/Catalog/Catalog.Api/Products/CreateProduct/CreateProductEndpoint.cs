using System.Collections.Immutable;

namespace Catalog.Api.Products.CreateProduct;

public readonly record struct CreateProductRequest()
{
    public string Name { get; init; } = string.Empty;
    public IReadOnlyList<string> Category { get; init; } = ImmutableArray<string>.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageFile { get; init; } = string.Empty;
    public decimal Price { get; init; } = 0;
}

public readonly record struct CreateProductResponse()
{
    public Guid Id { get; init; } = Guid.Empty;
}

public sealed class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create product")
        .WithDescription("Create product");
    }
}