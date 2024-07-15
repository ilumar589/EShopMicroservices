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

public readonly record struct SayHelloResponse()
{
    public string Message { get; init; } = "Hi from test point";
}

public sealed class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/entry", () => Task.FromResult(Results.Ok(new SayHelloResponse())))
        .WithName("EntryPoint")
        .Produces<SayHelloResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Entry point for testing")
        .WithDescription("Entry point for testing");
        
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