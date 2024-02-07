using Microsoft.AspNetCore.Http.HttpResults;

namespace CatalogApi.Extensions
{
    public static class DateTimeEndpoints
    {
        public static void RegisterDateTimeEndpoints(this WebApplication app)
        {
            var dateTimeGroup = app.MapGroup("/datetime");

            dateTimeGroup.MapGet("/getasync", async () => { return Results.Ok(await DateTimeDTO.GetCurrent()); });
            dateTimeGroup.MapGet("/getcurrent", () => new DateTimeDTO(DateTime.Now));
            dateTimeGroup.MapGet("/getdate", () => DateTime.Today.ToString("dddd"));
            dateTimeGroup.MapGet("/gettime", () => DateTime.Now.ToString("HH:mm:ss"));
            dateTimeGroup.MapGet("/getday", () => DateTime.Today.ToString("dddd"));

        }

    }
}
