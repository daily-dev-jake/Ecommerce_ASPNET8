using Ecommerce_ASPNET8.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_ASPNET8.Data
{
	public class ProductContext : DbContext
	{
		//Entity is a row
		public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
		// Props names are multiple
		public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().ToTable("Product"); // Overrides the model builder to create a singular 'Product' Table

		}
	}
}
