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
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> SubmitClaim(Claim claim)
		{
			if (ModelState.IsValid)
			{
				_context.Claims.Add(claim);
				await _context.SaveChangesAsync();
				// Redirect or return a success message
				return RedirectToAction("Claims"); // Redirect back to the Claims view
			}
			return View(claim); // Return the same view with validation errors
		}

		public async Task<JsonResult> GetLecturerDetails(int userId)
		{
			// Fetch the lecturer details from the database
			var lecturer = await _context.Lecturers
				.Include(l => l.User)
				.FirstOrDefaultAsync(l => l.UserID == userId);

			return Json(lecturer); // Return the lecturer details as JSON
		}
	}
}
