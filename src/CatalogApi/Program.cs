using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CatalogItemDb>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("SQLite")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetService<CatalogItemDb>();
    context?.Database.EnsureCreated();
}

app.RegisterCatalogItemsEndpoints();

app.Run();
