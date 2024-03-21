using Ecommerce_ASPNET8.Models;

namespace Ecommerce_ASPNET8.Services
{
	public interface ICartService
	{
		void AddItem(Product product, int quantity);
		void RemoveItem(string productId);
		void ClearCart();
		Cart GetCart();
	}
}
