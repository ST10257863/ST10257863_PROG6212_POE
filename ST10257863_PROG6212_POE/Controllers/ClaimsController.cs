using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using ST10257863_PROG6212_POE.Models.Tables;

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

		//Allows lecturers to submit their claims by checking session information, validating input, and saving it to the database.
		[HttpPost]
		public async Task<IActionResult> SubmitClaim()
		{
			var lecturerId = HttpContext.Session.GetInt32("LecturerID");

			if (!lecturerId.HasValue)
			{
				ModelState.AddModelError("", "Lecturer ID is missing from the session.");
				return RedirectToAction("Claims");
			}

			var claim = new Claim
			{
				LecturerId = lecturerId.Value,
				SubmissionDate = DateTime.Now,
				Status = "Pending"
			};

			if (decimal.TryParse(Request.Form["HoursWorked"], out var hoursWorked) && hoursWorked > 0)
			{
				claim.HoursWorked = hoursWorked;
			}
			else
			{
				ModelState.AddModelError("HoursWorked", "Hours worked must be greater than zero.");
			}

			if (decimal.TryParse(Request.Form["overtimeWorked"], out var overtimeHoursWorked) && overtimeHoursWorked >= 0)
			{
				claim.OvertimeHoursWorked = overtimeHoursWorked;
			}
			else
			{
				ModelState.AddModelError("OvertimeHoursWorked", "Overtime hours must be zero or greater.");
			}

			if (ModelState.IsValid)
			{
				_context.Claims.Add(claim);
				await _context.SaveChangesAsync();
				return RedirectToAction("Claims");
			}

			return RedirectToAction("Claims");
		}

		//Fetches lecturer details based on session data.
		[HttpGet]
		public async Task<JsonResult> GetLecturerDetails()
		{
			var userId = HttpContext.Session.GetInt32("UserID");

			if (!userId.HasValue)
			{
				return Json(null);
			}

			var lecturer = await _context.Lecturers
				.Include(l => l.User)
				.FirstOrDefaultAsync(l => l.UserID == userId);

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

			return Json(lecturerDetails);
		}

		//Retrieves all claims submitted by a lecturer.
		[HttpGet]
		public IActionResult LoadLecturerClaims()
		{
			var lecturerID = HttpContext.Session.GetInt32("LecturerID");

			var lecturerClaims = _context.Claims
				.Where(c => c.LecturerId == lecturerID)
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList();

			return Json(lecturerClaims);
		}

		//Calculates pay based on hours worked and overtime.
		[HttpPost]
		[Route("Claims/CalculatePay/{hoursWorked}/{overtimeWorked}")]
		public JsonResult CalculatePay(double hoursWorked, double overtimeWorked)
		{
			var lecturerID = HttpContext.Session.GetInt32("LecturerID");

			if (hoursWorked < 0 || overtimeWorked < 0)
			{
				return Json(new
				{
					error = "Invalid input values. Please enter valid hours."
				});
			}

			var hourlyRate = _context.Lecturers
				.Where(l => l.LecturerID == lecturerID)
				.Select(l => l.HourlyRate)
				.FirstOrDefault();

			var regularPay = hoursWorked * (int)hourlyRate;
			var overtimePay = overtimeWorked * (int)hourlyRate * 1.5;
			var totalPay = regularPay + overtimePay;

			return Json(new
			{
				regularPay = regularPay,
				overtimePay = overtimePay,
				totalPay = totalPay
			});
		}
	}
}
