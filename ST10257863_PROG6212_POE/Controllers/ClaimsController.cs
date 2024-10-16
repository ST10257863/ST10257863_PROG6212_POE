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
			// Retrieve the User ID from the session as an int
			var userId = HttpContext.Session.GetInt32("UserID");

			// Validate if User ID exists in session
			if (!userId.HasValue)
			{
				return Json(null); // Return null if User ID is not valid
			}

			// Fetch the lecturer details from the database using the userId
			var lecturer = await _context.Lecturers
				.Include(l => l.User) // Ensure to include User details
				.FirstOrDefaultAsync(l => l.UserID == userId);

			// Create a new object to return only the necessary fields
			var lecturerDetails = new
			{
				LecturerId = lecturer?.LecturerID,
				UserName = lecturer?.User.UserName,
				FirstName = lecturer?.User.FirstName,
				LastName = lecturer?.User.LastName,
				HourlyRate = lecturer?.HourlyRate,
				Department = lecturer?.Department,
				Campus = lecturer?.Campus
			};

			return Json(lecturerDetails); // Return the lecturer details as JSON
		}

	}
}
