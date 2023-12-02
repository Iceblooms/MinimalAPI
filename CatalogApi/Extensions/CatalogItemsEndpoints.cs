namespace CatalogApi.Extensions
{
    public static class CatalogItemsEndpoints
    {
        public static void RegisterCatalogItemsEndpoints(this WebApplication app)
        {
            var catalogItemsGroup = app.MapGroup("/catalog");
            catalogItemsGroup.MapGet("/", GetAllCatalogItems);
        }

        static async Task<IResult> GetAllCatalogItems(CatalogItemDb db)
        {
            return TypedResults.Ok(await db.Items.ToArrayAsync());
        }
    }
}
