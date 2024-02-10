using Entities.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreAppNew2.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RoleController : Controller
	{
		private readonly IServiceManager _manager;

		public RoleController(IServiceManager manager)
		{
			_manager = manager;
		}

		public IActionResult Index()
		{
			return View(_manager.AuthService.GetAllRoles);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(IdentityRole role)
		{
			_manager.AuthService.CreateRole(role);	
			return RedirectToAction("Index");
		}

		public IActionResult Delete(string id)
		{
			IdentityRole? role = _manager.AuthService.GetAllRoles.Where(p => p.Id == id).FirstOrDefault();
			if(role is not null)
			{
				_manager.AuthService.DeleteRole(role);
			}
			return RedirectToAction("Index");
		}
	}
}
