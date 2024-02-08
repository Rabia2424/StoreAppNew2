using Microsoft.AspNetCore.Mvc;

namespace StoreAppNew2.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
