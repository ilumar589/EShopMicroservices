using System.Text.Json.Serialization;
using Catalog.Api.Products.CreateProduct;

namespace Catalog.Api;

[JsonSerializable(typeof(CreateProductRequest))]
[JsonSerializable(typeof(CreateProductResponse))]
[JsonSerializable(typeof(SayHelloResponse))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default)]
internal partial class CatalogJsonContext : JsonSerializerContext;