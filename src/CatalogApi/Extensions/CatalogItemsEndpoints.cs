using Microsoft.AspNetCore.Http.HttpResults;

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
            return TypedResults.Ok(await db.Items.Select(ci => new CatalogItemDTO(ci)).ToArrayAsync());
        }

        static async Task<IResult> GetCatalogItem(int id, CatalogItemDb db)
        {
            return await db.Items.FindAsync(id)
                is CatalogItem item
                    ? TypedResults.Ok(new CatalogItemDTO(item))
                    : TypedResults.NotFound();
        }

        static async Task<IResult> CreateCatalogItem(CatalogItemDTO catalogItemDTO, CatalogItemDb db)
        {
            var catalogItem = new CatalogItem
            {
                Name = catalogItemDTO.Name,
                Description = catalogItemDTO.Description,
            };

            db.Items.Add(catalogItem);
            await db.SaveChangesAsync();

            catalogItemDTO = new CatalogItemDTO(catalogItem);
            return TypedResults.Created($"/catalog/{catalogItem.Id}", catalogItemDTO);
        }

        static async Task<IResult> UpdateCatalogItem(int id, CatalogItemDTO inputCatalogItemDTO, CatalogItemDb db)
        {
            var catalogItem = await db.Items.FindAsync(id);

            if (catalogItem is null) return TypedResults.NotFound();

            catalogItem.Name = inputCatalogItemDTO.Name;
            catalogItem.Description = inputCatalogItemDTO.Description;

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
