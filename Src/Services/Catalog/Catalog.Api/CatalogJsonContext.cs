using System.Text.Json.Serialization;
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api;

[JsonSerializable(typeof(CreateProductRequest))]
[JsonSerializable(typeof(CreateProductResponse))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class CatalogJsonContext : JsonSerializerContext;