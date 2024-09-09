using Microsoft.AspNetCore.Mvc;
using ST10257863_PROG6212_POE.Models;
using System.Diagnostics;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Login()
		{
			return View();
		}

		public IActionResult Claims()
		{
			return View();
		}
		public IActionResult Verification()
		{
			return View();
		}

		public IActionResult Approval()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
