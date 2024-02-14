using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using StoreAppNew2.Models;

namespace StoreAppNew.Controllers
{
	public class ProductController : Controller
	{
		private readonly IServiceManager _manager;

		public ProductController(IServiceManager manager)
		{
			_manager = manager;
		}

		
		public IActionResult Index(ProductRequestParameters p)
		{
			var products = _manager.ProductService.GetAllProductsWithDetails(p);
			var pagination = new Pagination()
			{
				CurrentPage = p.PageNumber,
				ItemsPerPage = p.PageSize,
				TotalItems = _manager.ProductService.GetAllProducts(false).Count()
			};
			return View(new ProductListViewModel()
			{
				Products = products,
				Pagination = pagination
			});
		}

		//[HttpPost] //If we used that all the parameters in the productfiltermenu(in shared folder in default.cshtml) will not reflect to the url.
		//public IActionResult Index(ProductRequestParameters p, bool isActive)
		//{
		//	var model = _manager.ProductService.GetAllProductsWithDetails(p);
		//	return View(model);
		//}


		public IActionResult Get([FromRoute(Name = "id")] int id)
		{
			var model = _manager.ProductService.GetOneProduct(id, false);
			ViewData["Title"] = model?.ProductName;
			return View(model);
		}
	}
}
