using Ecommerce_ASPNET8.Data;
using Ecommerce_ASPNET8.Models;
using Ecommerce_ASPNET8.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_ASPNET8.Controllers
{
	public class CartController : Controller
	{
		private readonly CartService _cartService;
		private readonly ProductContext _productContext;
		public CartController(CartService cartService, ProductContext productContext)
		{
			_cartService = cartService;
			_productContext = productContext;
		}
		public IActionResult Index()
		{
			var cart = _cartService.GetCart();
			return View(cart);
		}
		[HttpPost]
		public IActionResult AddToCart([FromBody] AddToCartViewModel model)
		{
			if (ModelState.IsValid)
			{

				var cart = _cartService.GetCart();
				// Check if product exist in cart
				var existingItem = cart.Items.FirstOrDefault();
				if (existingItem != null)
				{
					// Update quantity of existing item
					existingItem.Quantity = model.Quantity;
					_cartService.UpdateCart(cart);

					// Return a JSON response with the updated flag
					return Json(new { updated = true });
				}
				// If product does not have item in cart
				_cartService.AddToCart(model.ProductId, model.Quantity);
				_cartService.UpdateCart(cart);

				// Return a JSON response with the updated flag
				return Json(new { updated = false });
			}
			// Model validation failed
			return BadRequest();

		}
		public IActionResult Remove(string productId)
		{
			_cartService.RemoveItem(productId);
			return RedirectToAction("Index", "Cart");
		}

	}
}
