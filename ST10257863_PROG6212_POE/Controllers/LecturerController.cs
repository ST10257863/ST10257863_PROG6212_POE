using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class LecturerController : Controller
	{
		private readonly AppDbContext _context;

		public LecturerController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> GetAllLecturers()
		{
			var lecturers = await _context.Lecturers
				.Include(l => l.User)  // Ensure User data is fetched with Lecturer
				.Select(l => new
				{
					l.LecturerID,
					l.User.FirstName,
					l.User.LastName,
					l.Department,
					l.Campus,
					l.HourlyRate
				})
				.ToListAsync();

			return Json(lecturers);
		}

		[HttpPost]
		public async Task<IActionResult> UpdateLecturer([FromBody] Lecturer updatedLecturer)
		{
			// Retrieve the existing lecturer, including related User data
			var existingLecturer = await _context.Lecturers
				.Include(l => l.User)
				.FirstOrDefaultAsync(l => l.LecturerID == updatedLecturer.LecturerID);

			if (existingLecturer == null)
			{
				// Lecturer not found, return failure status
				return NotFound(new
				{
					Message = "Lecturer not found"
				});
			}

			// Update lecturer properties
			existingLecturer.Department = updatedLecturer.Department;
			existingLecturer.Campus = updatedLecturer.Campus;
			existingLecturer.HourlyRate = updatedLecturer.HourlyRate;

			// Update user information if necessary
			existingLecturer.User.FirstName = updatedLecturer.User.FirstName;
			existingLecturer.User.LastName = updatedLecturer.User.LastName;

			// Save changes to the database
			await _context.SaveChangesAsync();

			// Return success status
			return Ok(new
			{
				Message = "Lecturer details updated successfully"
			});
		}
	}
}
