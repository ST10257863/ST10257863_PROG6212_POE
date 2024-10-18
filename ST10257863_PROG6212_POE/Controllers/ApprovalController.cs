using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using ST10257863_PROG6212_POE.Models.Tables;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class ApprovalController : Controller
	{
		private readonly AppDbContext _context;

		public ApprovalController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Approval()
		{
			return View();
		}

		//Fetches and returns all verified claims as a JSON object. Includes related lecturer and coordinator data.
		[HttpGet]
		public IActionResult GetAllVerifiedClaims()
		{
			var claimVerifications = _context.ClaimVerifications
				.Include(c => c.Claim)
					.ThenInclude(cl => cl.Lecturer)
						.ThenInclude(l => l.User)
				.Include(c => c.Coordinator)
					.ThenInclude(co => co.User)
				.Where(c => c.VerificationStatus == "Verified")
				.Select(c => new
				{
					verificationId = c.VerificationID,
					ClaimId = c.Claim.ClaimId,
					FullName = $"{c.Claim.Lecturer.User.FirstName} {c.Claim.Lecturer.User.LastName}",
					VerificationDate = c.VerificationDate,
					claimStatus = c.VerificationStatus
				})
				.ToList();

			return Json(claimVerifications);
		}

		//Retrieves specific claim details using the verification ID, including lecturer and coordinator information, and returns them as JSON.
		[HttpGet("Approval/GetVerifiedClaimDetails/{verificationId}")]
		public IActionResult GetVerifiedClaimDetails(int verificationId)
		{
			var claimVerification = _context.ClaimVerifications
				.Include(cv => cv.Claim)
				.ThenInclude(c => c.Lecturer)
				.ThenInclude(l => l.User)
				.Include(cv => cv.Coordinator)
				.ThenInclude(co => co.User)
				.FirstOrDefault(cv => cv.VerificationID == verificationId);

			if (claimVerification == null)
			{
				return RedirectToAction("Approval");
			}

			var claimDetails = new
			{
				LecturerId = claimVerification.Claim.Lecturer.LecturerID,
				UserName = claimVerification.Claim.Lecturer.User.UserName,
				FullName = $"{claimVerification.Claim.Lecturer.User.FirstName} {claimVerification.Claim.Lecturer.User.LastName}",
				HourlyRate = claimVerification.Claim.Lecturer.HourlyRate,
				Department = claimVerification.Claim.Lecturer.Department,
				Campus = claimVerification.Claim.Lecturer.Campus,
				RegularHours = claimVerification.Claim.HoursWorked,
				OvertimeHours = claimVerification.Claim.OvertimeHoursWorked,
				TotalHours = claimVerification.Claim.HoursWorked + claimVerification.Claim.OvertimeHoursWorked,
				RegularPay = claimVerification.Claim.HoursWorked * claimVerification.Claim.Lecturer.HourlyRate,
				OvertimePay = claimVerification.Claim.OvertimeHoursWorked * (claimVerification.Claim.Lecturer.HourlyRate * 1.5M),
				TotalPay = (claimVerification.Claim.HoursWorked * claimVerification.Claim.Lecturer.HourlyRate) + (claimVerification.Claim.OvertimeHoursWorked * claimVerification.Claim.Lecturer.HourlyRate * 1.5M),
				CoordinatorId = claimVerification.Coordinator.CoordinatorID,
				CoordinatorUserName = claimVerification.Coordinator.User.UserName,
				CoordinatorFullName = $"{claimVerification.Coordinator.User.FirstName} {claimVerification.Coordinator.User.LastName}",
				CoordinatorDepartment = claimVerification.Coordinator.Department,
				CoordinatorCampus = claimVerification.Coordinator.Campus,
				VerificationID = verificationId
			};

			return Json(claimDetails);

		}

		//Approves a verified claim, updates the claim status, and creates an entry in the ClaimApproval table
		[HttpPost("Approval/ApproveClaim/{verificationId}")]
		public IActionResult ApproveClaim(int verificationId)
		{
			var claimVerification = _context.ClaimVerifications
				.Include(cv => cv.Claim)
				.FirstOrDefault(cv => cv.VerificationID == verificationId);

			if (claimVerification == null)
			{
				return RedirectToAction("Approval");
			}

			var academicManagerID = HttpContext.Session.GetInt32("AcademicManagerID");
			if (academicManagerID == null)
			{
				return RedirectToAction("Approval");
			}

			var claimApproval = new ClaimApproval
			{
				ClaimID = claimVerification.Claim.ClaimId,
				ManagerID = (int)academicManagerID,
				ApprovalDate = DateTime.UtcNow,
				IsApproved = true,
				ApprovalStatus = "Approved",
				ApprovalComments = "Claim approved successfully."
			};

			_context.ClaimApprovals.Add(claimApproval);

			claimVerification.VerificationStatus = "Approved";

			var claim = claimVerification.Claim;
			claim.Status = "Approved";

			_context.SaveChanges();
			return RedirectToAction("Approval");
		}

		//Rejects a verified claim, updates the claim status, and creates an entry in the ClaimApproval table for the rejection.
		[HttpPost("Approval/RejectVerifiedClaim/{verificationId}")]
		public IActionResult RejectVerifiedClaim(int verificationId)
		{
			var claimVerification = _context.ClaimVerifications
				.Include(cv => cv.Claim)
				.FirstOrDefault(cv => cv.VerificationID == verificationId);

			if (claimVerification == null)
			{
				return RedirectToAction("Approval");
			}

			var academicManagerID = HttpContext.Session.GetInt32("AcademicManagerID");
			if (academicManagerID == null)
			{
				return RedirectToAction("Approval");
			}

			claimVerification.VerificationStatus = "Approved";

			var claim = claimVerification.Claim;
			claim.Status = "Rejected";

			var claimApproval = new ClaimApproval
			{
				ClaimID = claim.ClaimId,
				ManagerID = (int)academicManagerID,
				ApprovalDate = DateTime.UtcNow,
				IsApproved = false,
				ApprovalStatus = "Rejected",
				ApprovalComments = "Claim rejected due to [reason]."
			};

			_context.ClaimApprovals.Add(claimApproval);

			_context.SaveChanges();

			return RedirectToAction("Approval");
		}
	}
}
