namespace Ecommerce_ASPNET8.Models
{
	public class Product
	{
		public required string Id { get; set; }
		public required string Name { get; set; }
		public required string Photo { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
	}
}
