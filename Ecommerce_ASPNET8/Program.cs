using Ecommerce_ASPNET8.Data;
using Ecommerce_ASPNET8.Models;
using Ecommerce_ASPNET8.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_ASPNET8
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ProductContext>(
				options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();
			builder.Services.AddHttpContextAccessor();

			builder.Services.AddTransient<CartService>();
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var context = services.GetRequiredService<ProductContext>();
					DbInit.Initialize(context);
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "Error occurred while creating DB");
				}
			}

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseSession();
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Products}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
