using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using Services;
using Microsoft.AspNetCore.Identity;

namespace StoreAppNew2.Infrastructure.Extensions
{
    public static class ServiceExtension
	{
		public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			// DbContext'in bağlanacağı veritabanı bağlantı dizesini alın
			string connectionString = configuration.GetConnectionString("DefaultConnection");

			// DbContext'i servis olarak ekleyin
			services.AddDbContext<RepositoryContext>(options =>
			{
				options.UseSqlServer(connectionString, b => b.MigrationsAssembly("StoreAppNew2"));

				options.EnableSensitiveDataLogging(true);
			});
		}

		public static void ConfigureIdentity(this IServiceCollection services)
		{
			services.AddIdentity<IdentityUser, IdentityRole>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.User.RequireUniqueEmail = true;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
			})
			.AddEntityFrameworkStores<RepositoryContext>();
		}

		public static void ConfigureSession(this IServiceCollection services)
		{
			//This two lines for Session part
			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.Cookie.Name = "StoreApp.Session";
				options.IdleTimeout = TimeSpan.FromMinutes(10);
			});
		}

		public static void ConfigureRepositoryRegistration(this IServiceCollection services)
		{
			services.AddScoped<IRepositoryManager, RepositoryManager>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
		}

		public static void ConfigureServiceRegistration(this IServiceCollection services)
		{
			services.AddScoped<IServiceManager, ServiceManager>();
			services.AddScoped<IProductService, ProductManager>();
			services.AddScoped<ICategoryService, CategoryManager>();
			services.AddScoped<IOrderService, OrderManager>();
			services.AddScoped<IAuthService, AuthManager>();
		}

		public static void ConfigureRouting(this IServiceCollection services)
		{
			services.AddRouting(options =>
			{
				options.LowercaseUrls = true;
			});
		}
	}
}
