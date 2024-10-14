using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using ST10257863_PROG6212_POE.Models;
using System.Threading.Tasks;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class LoginController : Controller
	{
		private readonly AppDbContext _context;

		public LoginController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(string username, string password)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

			if (user != null)
			{
				// Store UserID in session
				HttpContext.Session.SetInt32("UserID", user.UserID);
				return RedirectToAction("Claims", "Claims"); // Redirect to Claims page
			}

			ViewData["LoginError"] = "Invalid login attempt."; // Set error message
			return View(); // Return the same view with the error
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Remove("UserID"); // Clear the session
			return RedirectToAction("Login", "Login"); // Redirect to login page
		}
	}
}
