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
		public async Task<IActionResult> SubmitClaim()
		{
			// Fetch the Lecturer ID from the session
			var lecturerId = HttpContext.Session.GetInt32("LecturerID");

			// Check if Lecturer ID is present
			if (!lecturerId.HasValue)
			{
				ModelState.AddModelError("", "Lecturer ID is missing from the session.");
				return RedirectToAction("Claims"); // Redirect to Claims view
			}

			// Create a new Claim object with default values
			var claim = new Claim
			{
				LecturerId = lecturerId.Value,
				SubmissionDate = DateTime.Now,
				Status = "Pending"
			};

			// Get and validate HoursWorked from form data
			if (decimal.TryParse(Request.Form["HoursWorked"], out var hoursWorked) && hoursWorked > 0)
			{
				claim.HoursWorked = hoursWorked; // Assign valid HoursWorked
			}
			else
			{
				ModelState.AddModelError("HoursWorked", "Hours worked must be greater than zero.");
			}

			// Get and validate OvertimeHoursWorked from form data
			if (decimal.TryParse(Request.Form["overtimeWorked"], out var overtimeHoursWorked) && overtimeHoursWorked >= 0)
			{
				claim.OvertimeHoursWorked = overtimeHoursWorked;
			}
			else
			{
				ModelState.AddModelError("OvertimeHoursWorked", "Overtime hours must be zero or greater.");
			}

			// Check if the model is valid
			if (ModelState.IsValid)
			{
				// Save the new claim to the database
				_context.Claims.Add(claim);
				await _context.SaveChangesAsync();
				return RedirectToAction("Claims"); // Redirect after successful submission
			}

			return RedirectToAction("Claims"); // Redirect back if validation fails
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

		[HttpGet]
		public IActionResult LoadLecturerClaims()
		{
			var lecturerID = HttpContext.Session.GetInt32("LecturerID");

			var lecturerClaims = _context.Claims
				.Where(c => c.LecturerId == lecturerID) // Move the Where clause before Select
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList(); // Ensure ToList() is after Select

			return Json(lecturerClaims);
		}

		[HttpPost]
		[Route("Claims/CalculatePay/{hoursWorked}/{overtimeWorked}")]
		public JsonResult CalculatePay(double hoursWorked, double overtimeWorked)
		{
			var lecturerID = HttpContext.Session.GetInt32("LecturerID");

			// Validate input
			if (hoursWorked < 0 || overtimeWorked < 0)
			{
				return Json(new
				{
					error = "Invalid input values. Please enter valid hours."
				});
			}

			// Assuming you get hourlyRate from the database or session
			var hourlyRate = _context.Lecturers
				.Where(l => l.LecturerID == lecturerID) // filter by LecturerID
				.Select(l => l.HourlyRate) // select the hourly rate
				.FirstOrDefault(); // get the first or default value
								   // Calculate pay
			var regularPay = hoursWorked * (int)hourlyRate;
			var overtimePay = overtimeWorked * (int)hourlyRate * 1.5;
			var totalPay = regularPay + overtimePay;

			// Return the calculated values as JSON
			return Json(new
			{
				regularPay = regularPay,
				overtimePay = overtimePay,
				totalPay = totalPay
			});
		}
	}
}
