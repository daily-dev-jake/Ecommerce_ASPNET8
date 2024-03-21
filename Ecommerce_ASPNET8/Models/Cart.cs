namespace Ecommerce_ASPNET8.Models
{
	public class Cart
	{
		public string ID { get; set; }
		public List<Item> Items { get; set; }
		public double TotalCost => Items.Sum(items => items.Cost);
		public Cart()
		{
			Items = new List<Item>();
		}
	}
}
