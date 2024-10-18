using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using ST10257863_PROG6212_POE.Models.Tables;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class VerificationController : Controller
	{
		private readonly AppDbContext _context;

		public VerificationController(AppDbContext context)
		{
			_context = context;
		}

		// Displays the verification view.
		public IActionResult Verification()
		{
			return View();
		}

		// Retrieves all claims with a "Pending" status and associated lecturer information.
		[HttpGet]
		public IActionResult GetAllClaims()
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

		// Retrieves detailed information about a specific claim, including lecturer and user data.
		[HttpGet]
		[Route("Verification/GetClaimDetails/{claimId}")]
		public IActionResult GetClaimDetails(int claimId)
		{
			var claim = _context.Claims
				.Include(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return RedirectToAction("Verification");
			}

			var claimDetails = new
			{
				LecturerId = claim.Lecturer.LecturerID,
				UserName = claim.Lecturer.User.UserName,
				FullName = $"{claim.Lecturer.User.FirstName} {claim.Lecturer.User.LastName}",
				HourlyRate = claim.Lecturer.HourlyRate,
				Department = claim.Lecturer.Department,
				Campus = claim.Lecturer.Campus,
				RegularHours = claim.HoursWorked,
				OvertimeHours = claim.OvertimeHoursWorked,
				TotalHours = claim.HoursWorked + claim.OvertimeHoursWorked,
				RegularPay = claim.HoursWorked * claim.Lecturer.HourlyRate,
				OvertimePay = claim.OvertimeHoursWorked * (claim.Lecturer.HourlyRate * 1.5M),
				TotalPay = (claim.HoursWorked * claim.Lecturer.HourlyRate) + (claim.OvertimeHoursWorked * claim.Lecturer.HourlyRate * 1.5M)
			};
			return Json(claimDetails); // Return the claim details as JSON
		}

		// Accepts a claim, updates its status to "Verified," and records the verification in the database.
		[HttpPost]
		public IActionResult AcceptClaim(int claimId)
		{
			var claim = _context.Claims
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return RedirectToAction("Verification");
			}

			var coordinatorId = HttpContext.Session.GetInt32("CoordinatorID");

			if (coordinatorId == null)
			{
				return RedirectToAction("Verification");
			}

			claim.Status = "Verified";

			// Create and save the claim verification entry
			var claimVerification = new ClaimVerification
			{
				ClaimID = claim.ClaimId,
				CoordinatorID = (int)coordinatorId,
				VerificationDate = DateTime.UtcNow,
				VerificationStatus = "Verified",
				IsVerified = true,
				VerificationComments = "Claim accepted successfully."
			};

			_context.ClaimVerifications.Add(claimVerification);
			_context.SaveChanges();
			return RedirectToAction("Verification");
		}

		// Rejects a claim and updates its status to "Rejected."
		[HttpPost]
		public IActionResult RejectClaim(int claimId)
		{
			var claim = _context.Claims
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return RedirectToAction("Verification");
			}

			claim.Status = "Rejected";  // Update status for rejected claims
			_context.SaveChanges();  // Ensure changes are saved

			return RedirectToAction("Verification");
		}
	}
}
