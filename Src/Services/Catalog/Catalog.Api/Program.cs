using Catalog.Api;
using Catalog.Api.Products.CreateProduct;
using Catalog.Api.Products.GetProducts;
using Weasel.Core;

var builder = WebApplication.CreateSlimBuilder(args);

builder.WebHost.UseKestrelHttpsConfiguration();

// Add services to container

builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, CatalogJsonContext.Default));

builder.Services.AddCarter(configurator: config =>
{
    config.WithModule<CreateProductEndpoint>();
    config.WithModule<GetProductsEndpoint>();
});

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.Run();

