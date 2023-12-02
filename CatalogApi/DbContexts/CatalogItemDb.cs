namespace CatalogApi.DbContexts
{
    public class CatalogItemDb(DbContextOptions<CatalogItemDb> options) : DbContext(options)
    {
        public DbSet<CatalogItem> Items => Set<CatalogItem>();
    }
}
