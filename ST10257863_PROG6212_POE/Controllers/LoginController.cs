using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
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

				// Assign IDs based on user role
				await AssignUserIdsToSession(user.UserID);

				return RedirectToAction("Claims", "Claims"); // Redirect to Claims page
			}

			ViewData["LoginError"] = "Invalid login attempt."; // Set error message
			return View(); // Return the same view with the error
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Remove("UserID"); // Clear the session
			HttpContext.Session.Remove("LecturerID");
			HttpContext.Session.Remove("AcademicManagerID");
			HttpContext.Session.Remove("CoordinatorID");
			return RedirectToAction("Login", "Login"); // Redirect to login page
		}

		private async Task AssignUserIdsToSession(int userId)
		{
			// Check if the user is a Lecturer
			var lecturer = await _context.Lecturers.FirstOrDefaultAsync(l => l.UserID == userId);
			if (lecturer != null)
			{
				HttpContext.Session.SetInt32("LecturerID", lecturer.LecturerID);
				Console.WriteLine($"Lecturer ID assigned: {lecturer.LecturerID}"); // Debugging statement
			}
			else
			{
				Console.WriteLine("No Lecturer found for the given UserID."); // Debugging statement
			}

			// Check if the user is an Academic Manager
			var academicManager = await _context.AcademicManagers.FirstOrDefaultAsync(am => am.UserID == userId);
			if (academicManager != null)
			{
				HttpContext.Session.SetInt32("AcademicManagerID", academicManager.ManagerID);
				Console.WriteLine($"Academic Manager ID assigned: {academicManager.ManagerID}"); // Debugging statement
			}
			else
			{
				Console.WriteLine("No Academic Manager found for the given UserID."); // Debugging statement
			}

			// Check if the user is a Coordinator
			var coordinator = await _context.Coordinators.FirstOrDefaultAsync(c => c.UserID == userId);
			if (coordinator != null)
			{
				HttpContext.Session.SetInt32("CoordinatorID", coordinator.CoordinatorID);
				Console.WriteLine($"Coordinator ID assigned: {coordinator.CoordinatorID}"); // Debugging statement
			}
			else
			{
				Console.WriteLine("No Coordinator found for the given UserID."); // Debugging statement
			}
		}

	}
}
