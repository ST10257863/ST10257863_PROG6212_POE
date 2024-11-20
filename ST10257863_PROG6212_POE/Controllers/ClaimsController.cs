using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using ST10257863_PROG6212_POE.Models.Tables;
using System.IO;

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

		// Allows lecturers to submit their claims by checking session information, validating input, and saving it to the database.
		[HttpPost]
		public async Task<IActionResult> SubmitClaim(List<IFormFile> documents) // Accept multiple files
		{
			var lecturerId = HttpContext.Session.GetInt32("LecturerID");

			if (!lecturerId.HasValue)
			{
				ModelState.AddModelError("", "Lecturer ID is missing from the session.");
				return View("Claims"); // Render the same view with error message
			}

			var claim = new Claim
			{
				LecturerId = lecturerId.Value,
				SubmissionDate = DateTime.Now,
				Status = "Pending",
				SupportingDocuments = new List<string>() // Initialize the list for supporting documents
			};

			if (decimal.TryParse(Request.Form["HoursWorked"], out var hoursWorked) && hoursWorked > 0)
			{
				claim.HoursWorked = hoursWorked;
			}
			else
			{
				ModelState.AddModelError("HoursWorked", "Hours worked must be greater than zero.");
			}

			if (decimal.TryParse(Request.Form["OvertimeWorked"], out var overtimeHoursWorked) && overtimeHoursWorked >= 0)
			{
				claim.OvertimeHoursWorked = overtimeHoursWorked;
			}
			else
			{
				ModelState.AddModelError("OvertimeHoursWorked", "Overtime hours must be zero or greater.");
			}

			if (ModelState.IsValid)
			{
				// Save uploaded documents if any
				if (documents != null && documents.Count > 0)
				{
					var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

					// Ensure the directory exists
					if (!Directory.Exists(uploadPath))
					{
						Directory.CreateDirectory(uploadPath);
					}

					foreach (var document in documents)
					{
						if (document.Length > 0)
						{
							var fileName = Path.GetFileName(document.FileName);
							var filePath = Path.Combine(uploadPath, fileName);

							try
							{
								using (var stream = new FileStream(filePath, FileMode.Create))
								{
									await document.CopyToAsync(stream);
								}

								claim.SupportingDocuments.Add(filePath); // Store file path in the database
							}
							catch (Exception ex)
							{
								ModelState.AddModelError("", $"Error uploading file: {ex.Message}");
								return View("Claims");
							}
						}
					}
				}

				// Save the claim to the database
				_context.Claims.Add(claim);
				await _context.SaveChangesAsync();

				return RedirectToAction("Claims");
			}

			return View("Claims");
		}

		// Fetches lecturer details based on session data.
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

			if (lecturer == null)
			{
				return Json(null);
			}

			var lecturerDetails = new
			{
				LecturerId = lecturer.LecturerID,
				UserName = lecturer.User.UserName,
				FirstName = lecturer.User.FirstName,
				LastName = lecturer.User.LastName,
				HourlyRate = lecturer.HourlyRate,
				Department = lecturer.Department,
				Campus = lecturer.Campus
			};

			return Json(lecturerDetails);
		}

		// Retrieves all claims submitted by a lecturer.
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

		// Calculates pay based on hours worked and overtime.
		[HttpPost]
		[Route("Claims/CalculatePay/{hoursWorked}/{overtimeWorked?}")]
		public JsonResult CalculatePay(double hoursWorked, double? overtimeWorked = null)
		{
			var lecturerID = HttpContext.Session.GetInt32("LecturerID");

			if (hoursWorked < 0 || (overtimeWorked.HasValue && overtimeWorked < 0))
			{
				return Json(new
				{
					error = "Invalid input values. Please enter valid hours."
				});
			}

			overtimeWorked ??= 0;

			var hourlyRate = _context.Lecturers
				.Where(l => l.LecturerID == lecturerID)
				.Select(l => l.HourlyRate)
				.FirstOrDefault();

			var regularPay = hoursWorked * (int)hourlyRate;
			var overtimePay = overtimeWorked.Value * (int)hourlyRate * 1.5; // .Value because overTimeWorked can be null
			var totalPay = regularPay + overtimePay;

			return Json(new
			{
				regularPay = regularPay,
				overtimePay = overtimePay,
				totalPay = totalPay
			});
		}

		// Retrieves all claims with a "Pending" status and associated lecturer information.
		[HttpGet]
		public IActionResult GetAllPendingClaims()
		{
			var claims = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.Where(c => c.Status == "Pending")
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList();

			return Json(claims);
		}

		// Retrieves all claims with a "Verified" status and associated lecturer information.
		[HttpGet]
		public IActionResult GetAllVerifiedClaims()
		{
			var claims = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.Where(c => c.Status == "Verified")
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList();

			return Json(claims);
		}

		// Retrieves all claims with a "Approved" status and associated lecturer information.
		[HttpGet]
		public IActionResult GetAllApprovedClaims()
		{
			var claims = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.Where(c => c.Status == "Approved")
				.Select(c => new
				{
					c.ClaimId,
					c.SubmissionDate,
					c.Status
				})
				.ToList();

			return Json(claims);
		}

		// Retrieves detailed information about a specific claim, including lecturer and user data.
		[HttpGet]
		[Route("Claims/GetClaimDetails/{claimId}")]
		public IActionResult GetClaimDetails(int claimId)
		{
			var claim = _context.Claims
				.Include(c => c.Lecturer)
					.ThenInclude(l => l.User)  // Include associated User for the Lecturer
				.Include(c => c.Manager)  // Include AcademicManager for approval
				.Include(c => c.Coordinator)  // Include Coordinator for verification
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return NotFound("Claim not found.");
			}

			var claimDetails = new
			{
				// Lecturer and User details
				LecturerId = claim.Lecturer.LecturerID,
				FullName = $"{claim.Lecturer.User.FirstName} {claim.Lecturer.User.LastName}",
				HourlyRate = claim.Lecturer.HourlyRate,
				Department = claim.Lecturer.Department,
				Campus = claim.Lecturer.Campus,

				// Claim details
				RegularHours = claim.HoursWorked,
				OvertimeHours = claim.OvertimeHoursWorked,
				TotalHours = claim.HoursWorked + claim.OvertimeHoursWorked,
				RegularPay = claim.HoursWorked * claim.Lecturer.HourlyRate,
				OvertimePay = claim.OvertimeHoursWorked * (claim.Lecturer.HourlyRate * 1.5M),
				TotalPay = (claim.HoursWorked + claim.OvertimeHoursWorked) * claim.Lecturer.HourlyRate,

				// Approval details
				ManagerId = claim.Manager?.ManagerID,
				ManagerFullName = $"{claim.Manager?.User.FirstName} {claim.Manager?.User.LastName}",
				ManagerDepartment = claim.Manager?.Department,
				ManagerCampus = claim.Manager?.Campus,
				ApprovalDate = claim.ApprovalDate,
				IsApproved = claim.IsApproved,
				ApprovalComments = claim.ApprovalComments,


				// Verification details
				CoordinatorId = claim.Coordinator?.CoordinatorID,
				CoordinatorFullName = $"{claim.Coordinator?.User.FirstName} {claim.Coordinator?.User.LastName}",
				CoordinatorDepartment = claim.Coordinator?.Department,
				CoordinatorCampus = claim.Coordinator?.Campus,
				VerificationDate = claim.VerificationDate,
				IsVerified = claim.IsVerified,
				VerificationComments = claim.VerificationComments,


				// Claim supporting documents
				SupportingDocuments = claim.SupportingDocuments
			};

			return Json(claimDetails);
		}
	}
}
