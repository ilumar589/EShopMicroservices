var builder = WebApplication.CreateSlimBuilder(args);

// Add services to container

var app = builder.Build();

// Configure the HTTP request pipeline

app.Run();

