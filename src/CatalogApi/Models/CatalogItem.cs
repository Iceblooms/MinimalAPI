﻿namespace CatalogApi.Models
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ModelSecret { get; set; }
    }
}
