namespace Ecommerce_ASPNET8.Models
{
	public class Item
	{
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public double Cost
		{
			get
			{
				return Product.Price * Quantity;
			}
		}
	}
}
