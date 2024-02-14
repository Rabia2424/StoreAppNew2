using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreAppNew2.Components
{
	public class CategorySummaryViewComponent : ViewComponent
	{
		private readonly IServiceManager _manager;

		public CategorySummaryViewComponent(IServiceManager manager)
		{
			_manager = manager;
		}

		public string Invoke()
		{
			return _manager.ProductService.GetAllProducts(false).Count().ToString();
		}
	}
}
