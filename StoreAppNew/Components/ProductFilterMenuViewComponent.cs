using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreAppNew2.Components
{
	public class ProductFilterMenuViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
