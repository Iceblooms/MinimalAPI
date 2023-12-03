namespace CatalogApi.Extensions
{
    public static class CatalogItemsEndpoints
    {
        public static void RegisterCatalogItemsEndpoints(this WebApplication app)
        {
            var catalogItemsGroup = app.MapGroup("/catalog");

            catalogItemsGroup.MapGet("/", GetAllCatalogItems);
            catalogItemsGroup.MapGet("/{id}", GetCatalogItem);
            catalogItemsGroup.MapPost("/", CreateCatalogItem);
            catalogItemsGroup.MapPut("/{id}", UpdateCatalogItem);
            catalogItemsGroup.MapDelete("/{id}", DeleteCatalogItem);
        }

        static async Task<IResult> GetAllCatalogItems(CatalogItemDb db)
        {
            return TypedResults.Ok(await db.Items.ToArrayAsync());
        }

        static async Task<IResult> GetCatalogItem(int id, CatalogItemDb db)
        {
            return await db.Items.FindAsync(id)
                is CatalogItem item
                    ? TypedResults.Ok(item)
                    : TypedResults.NotFound();
        }

        static async Task<IResult> CreateCatalogItem(CatalogItem catalogItem, CatalogItemDb db)
        {
            db.Items.Add(catalogItem);
            await db.SaveChangesAsync();

            return TypedResults.Created($"/catalog/{catalogItem.Id}", catalogItem);
        }

        static async Task<IResult> UpdateCatalogItem(int id, CatalogItem inputCatalogItem, CatalogItemDb db)
        {
            var catalogItem = await db.Items.FindAsync(id);

            if (catalogItem is null) return TypedResults.NotFound();

            catalogItem.Name = inputCatalogItem.Name;
            catalogItem.Description = inputCatalogItem.Description;

            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        static async Task<IResult> DeleteCatalogItem(int id, CatalogItemDb db)
        {
            if (await db.Items.FindAsync(id) is CatalogItem todo)
            {
                db.Items.Remove(todo);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
