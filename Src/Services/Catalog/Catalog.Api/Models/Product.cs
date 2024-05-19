namespace Catalog.Api.Models;

public sealed class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Category { get; set; } = Enumerable.Empty<string>().ToList();
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
}