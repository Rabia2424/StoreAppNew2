using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services;
using Services.Contracts;
using StoreAppNew2.Infrastructure.Extensions;
using StoreAppNew2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//This line added for razor pages
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//// DbContext'in baðlanacaðý veritabaný baðlantý dizesini alýn
//string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//// DbContext'i servis olarak ekleyin
//builder.Services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("StoreAppNew2")));

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();

////This two lines for Session part
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//	options.Cookie.Name = "StoreApp.Session";
//	options.IdleTimeout = TimeSpan.FromMinutes(10);
//});

builder.Services.ConfigureSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//builder.Services.AddScoped<IServiceManager, ServiceManager>();
//builder.Services.AddScoped<IProductService, ProductManager>();
//builder.Services.AddScoped<ICategoryService, CategoryManager>();
//builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureRouting();
builder.Services.ConfigureApplicationCookie();


builder.Services.AddScoped<Cart>(c => SessionCart.GetCart(c));

builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapAreaControllerRoute(
		name : "Admin",
		areaName : "Admin",
		pattern : "Admin/{controller=Dashboard}/{action=Index}/{id?}"
		);

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");

	//Endpoint For Razor Page 
	endpoints.MapRazorPages();	
});

app.ConfigureAndCheckMigration();
app.ConfigureDefaultAdminUser();
app.Run();
