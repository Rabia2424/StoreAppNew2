using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreAppNew2.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
	{
		private readonly IServiceManager _manager;

		public CategoryController(IServiceManager manager)
		{
			_manager = manager;
		}

		public IActionResult Index()
		{
			var model = _manager.CategoryService.GetAllCategories(false);
			return View(model);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			_manager.CategoryService.CreateCategory(category);
			return RedirectToAction("Index");
		}
	}
}
