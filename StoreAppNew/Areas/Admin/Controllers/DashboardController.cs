﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreAppNew2.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			TempData["info"] = $"Welcome back, {DateTime.Now.ToShortTimeString()}";
			return View();
		}
	}
}
