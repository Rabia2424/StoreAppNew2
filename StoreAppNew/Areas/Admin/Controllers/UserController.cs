using Entities.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreAppNew2.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly IServiceManager _manager;

		public UserController(IServiceManager manager)
		{
			_manager = manager;	
		}
		public IActionResult Index()
		{
			return View(_manager.AuthService.GetAllUsers());
		}

		public IActionResult Create()
		{
			return View(new UserDtoForCreation()
			{
				Roles = new HashSet<string>(_manager
				.AuthService
				.GetAllRoles
				.Select(r => r.Name)
				.ToList())
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm]UserDtoForCreation userDto)
		{
			var result = await _manager.AuthService.CreateUser(userDto);
			return result.Succeeded
				? RedirectToAction("Index")
				: View();
		}

		public async Task<IActionResult> Update([FromRoute(Name ="id")]string id)
		{
			var user = await _manager.AuthService.GetOneUserForUpdate(id);
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]	
		public async Task<IActionResult> Update([FromForm]UserDtoForUpdate userD)
		{
			if(ModelState.IsValid)
			{
				await _manager.AuthService.Update(userD);
				return RedirectToAction("Index");
			}
			return View();	
		}
	}
}
