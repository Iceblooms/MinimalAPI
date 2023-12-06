using System.Text.Json.Serialization;

namespace CatalogApi.DTO
{
    public record CatalogItemDTO
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }

        [JsonConstructor]
        public CatalogItemDTO(int id, string? name, string? description)
        {
            Id = id; Name = name; Description = description;
        }

        public CatalogItemDTO(CatalogItem catalogItem)
            : this(catalogItem.Id, catalogItem.Name, catalogItem.Description)
        {
        }
    }
}