using System.Collections.Immutable;
using BuildingBlocks.CQRS;
using Catalog.Api.Models;

namespace Catalog.Api.Products.CreateProduct;

public readonly record struct CreateProductCommand() : ICommand<CreateProductResult>
{
    public string Name { get; init; } = string.Empty;
    public IReadOnlyList<string> Category { get; init; } = ImmutableArray<string>.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageFile { get; init; } = string.Empty;
    public decimal Price { get; init; } = 0;
}

public readonly record struct CreateProductResult()
{
    public Guid Id { get; init; } = Guid.Empty;
}

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Category = request.Category.ToList(),
            ImageFile = request.ImageFile,
            Price = request.Price
        };
        
        //TODO save to database

        return new CreateProductResult
        {
            Id = Guid.NewGuid()
        };
    }
}