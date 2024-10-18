using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class LoginController : Controller
	{
		private readonly AppDbContext _context;

		public LoginController(AppDbContext context)
		{
			_context = context;
		}

		// Login method: Verifies the user credentials, stores the user and role-based IDs in the session if successful, and handles redirection. If login fails, it displays an error.
		public IActionResult Login()
		{
			return View();
		}

		// Login method: Handles the login logic, checks credentials, and sets session data.
		[HttpPost]
		public async Task<IActionResult> Login(string username, string password)
		{
			var user = await _context.Users
				.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

			if (user != null)
			{
				// Store UserID in session
				HttpContext.Session.SetInt32("UserID", user.UserID);

				// Assign IDs based on user role
				await AssignUserIdsToSession(user.UserID);

				return RedirectToAction("Claims", "Claims");
			}

			ViewData["LoginError"] = "Invalid login attempt.";
			return View();
		}

		// Logout method: Clears session information and redirects to the login page.
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("UserID");
			HttpContext.Session.Remove("LecturerID");
			HttpContext.Session.Remove("AcademicManagerID");
			HttpContext.Session.Remove("CoordinatorID");
			return RedirectToAction("Login", "Login");
		}

		// AssignUserIdsToSession method: Checks if the user has specific roles and stores the respective role IDs in session.
		private async Task AssignUserIdsToSession(int userId)
		{
			// Check if the user is a Lecturer
			var lecturer = await _context.Lecturers.FirstOrDefaultAsync(l => l.UserID == userId);
			if (lecturer != null)
			{
				HttpContext.Session.SetInt32("LecturerID", lecturer.LecturerID);
			}

			// Check if the user is an Academic Manager
			var academicManager = await _context.AcademicManagers.FirstOrDefaultAsync(am => am.UserID == userId);
			if (academicManager != null)
			{
				HttpContext.Session.SetInt32("AcademicManagerID", academicManager.ManagerID);
			}

			// Check if the user is a Coordinator
			var coordinator = await _context.Coordinators.FirstOrDefaultAsync(c => c.UserID == userId);
			if (coordinator != null)
			{
				HttpContext.Session.SetInt32("CoordinatorID", coordinator.CoordinatorID);
			}
		}
	}
}
