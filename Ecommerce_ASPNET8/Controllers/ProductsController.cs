﻿using Ecommerce_ASPNET8.Data;
using Ecommerce_ASPNET8.Models;
using Ecommerce_ASPNET8.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_ASPNET8.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ProductContext _context;
		private readonly CartService _cartService;

		public ProductsController(ProductContext context, CartService cartService)
		{
			_context = context;
			this._cartService = cartService;
		}

		// GET: Products
		public async Task<IActionResult> Index()
		{
			var products = await _context.Products.ToListAsync();
			var cart = _cartService.GetCart();
			foreach (var item in cart.Items)
			{
				var product = products.FirstOrDefault(p => p.Id == item.Product.Id);
				if (product != null)
				{
					product.Quantity = item.Quantity;
				}
			}

			return View(products);
			//return _context.Products != null
			//	? View(await _context.Products.ToListAsync())
			//	: Problem("Entity set 'ProductContext.Products' is null");
		}

		// GET: Products/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Photo,Price")] Product product)
		{
			if (ModelState.IsValid)
			{
				_context.Add(product);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		// GET: Products/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Photo,Price")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(product);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductExists(product.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(product);
		}

		// GET: Products/Delete/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductExists(string id)
		{
			return _context.Products.Any(e => e.Id == id);
		}
	}
}
