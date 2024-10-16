using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using ST10257863_PROG6212_POE.Models.Tables;
using System.Threading.Tasks;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class ClaimsController : Controller
	{
		private readonly AppDbContext _context;

		public ClaimsController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Claims()
		{
			var userId = HttpContext.Session.GetString("UserID");
			ViewData["UserID"] = userId; // Store the User ID in ViewData for use in the view
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SubmitClaim(Claim claim)
		{
			if (ModelState.IsValid)
			{
				_context.Claims.Add(claim);
				await _context.SaveChangesAsync();
				return RedirectToAction("Claims"); // Redirect back to the Claims view
			}
			return View(claim); // Return the same view with validation errors
		}

		[HttpGet] // Ensure this method can respond to GET requests
		public async Task<JsonResult> GetLecturerDetails()
		{
			// Retrieve the User ID from the session
			var userIdString = HttpContext.Session.GetString("UserID");

			// Validate if User ID exists in session
			if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
			{
				return Json(null); // Return null if User ID is not valid
			}

			// Fetch the lecturer details from the database
			var lecturer = await _context.Lecturers
				.Include(l => l.User)
				.FirstOrDefaultAsync(l => l.UserID == userId);

			return Json(lecturer); // Return the lecturer details as JSON
		}
	}
}
