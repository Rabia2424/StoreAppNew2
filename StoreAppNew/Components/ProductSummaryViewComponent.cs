using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services.Contracts;

namespace StoreAppNew2.Components
{
	public class ProductSummaryViewComponent : ViewComponent
	{
		private readonly IServiceManager _manager;

		public ProductSummaryViewComponent(IServiceManager manager)
		{
			_manager = manager;
		}

		public string Invoke()
		{
			// service
			return _manager.ProductService.GetAllProducts(false).Count().ToString();
		}
	}
}
