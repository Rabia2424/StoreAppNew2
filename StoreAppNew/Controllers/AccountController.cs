using AutoMapper;
using Entities.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreAppNew2.Models;
using System.Linq;

namespace StoreAppNew2.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IMapper _mapper;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_mapper = mapper;
		}

		public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
		{
			return View(new LoginModel()
			{
				ReturnUrl = ReturnUrl
			});
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login([FromForm]LoginModel model)
		{
			if(ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(model.Name);
				if (user != null)
				{
					//Oturum açma
					await _signInManager.SignOutAsync();
					if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
					{
						return Redirect(model?.ReturnUrl ?? "/");
					}
					
				}
				ModelState.AddModelError("Error","Invalid username or password.");
			}

			return View();
		}

		public async Task<IActionResult> Logout([FromQuery(Name ="ReturnUrl")] string ReturnUrl="/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(ReturnUrl);
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register([FromForm]RegisterDto registerDto)
		{
			if (ModelState.IsValid)
			{
				//var user = new IdentityUser();
				//user = _mapper.Map<IdentityUser>(registerDto);
				var user = new IdentityUser()
				{
					UserName = registerDto.UserName,
					Email = registerDto.Email,
				};
				var result = await _userManager.CreateAsync(user, registerDto.Password);

				if(result.Succeeded)
				{
					var RoleResult = await _userManager
						.AddToRoleAsync(user, "User");
					if(RoleResult.Succeeded)
					{
						return RedirectToAction("Login", new {ReturnUrl = "/"});
					}
				}
				else
				{
                    foreach (var err in result.Errors)
                    {
						ModelState.AddModelError("",err.Description);
                    }
                }
			}
			return View();
		}
	}
}
