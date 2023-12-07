var builder = WebApplication.CreateBuilder(args);
builder.Services.AddKeyedSingleton<ICache, BigCache>("big");
builder.Services.AddKeyedSingleton<ICache, SmallCache>("small");
var app = builder.Build();

app.Logger.LogInformation("The app started");

var message = app.Configuration["Greeting"] ?? "Config failed!";

app.MapGet("/", () => $"{message} world!");
app.MapGet("/big", ([FromKeyedServices("big")] ICache bigCache) => bigCache.Get("date"));
app.MapGet("/small", ([FromKeyedServices("small")] ICache bigCache) => bigCache.Get("small"))
   .WithName("small"); // name must be globally unique; name are used as the OpenAPI operation id when OpenAPI support is enabled
app.MapGet("/discover", DiscoverLF);

app.Run();

string DiscoverLF(LinkGenerator linker) => $"The link to the small route is {linker.GetPathByName("small", values: null)}";

public interface ICache
{
    object Get(string key);
}

public class BigCache : ICache
{
    public object Get(string key) => $"Resolving {key} from big cache.";
}

public class SmallCache : ICache
{
    public object Get(string key) => $"Resolving {key} from small cache.";
}