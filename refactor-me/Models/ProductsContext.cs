namespace refactor_me.Models
{
    using System.Data.Entity;

    public partial class ProductsContext : DbContext
    {
        public ProductsContext() : base("name=DatabaseContext") { }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }
}