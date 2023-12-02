var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CatalogItemDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.RegisterCatalogItemsEndpoints();

app.Run();
