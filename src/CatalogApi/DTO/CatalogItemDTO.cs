namespace CatalogApi.DTO
{
    public record CatalogItemDTO(int Id, string? Name, string? Description)
    {
        public CatalogItemDTO(CatalogItem catalogItem)
            : this(catalogItem.Id, catalogItem.Name, catalogItem.Description)
        {
        }
    }
}