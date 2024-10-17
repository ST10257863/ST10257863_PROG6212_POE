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

		[HttpGet]
		public IActionResult GetAllVerifiedClaims()
		{
			// Retrieve all verified claims along with associated lecturer and coordinator information
			var claimVerifications = _context.ClaimVerifications
				.Include(c => c.Claim)
					.ThenInclude(cl => cl.Lecturer) // Include Lecturer information
						.ThenInclude(l => l.User) // Include User information related to the Lecturer
				.Include(c => c.Coordinator) // Include Coordinator details
					.ThenInclude(co => co.User) // Include User information related to the Coordinator
				.Where(c => c.VerificationStatus == "Verified") // Changed to Verified
				.Select(c => new
				{
					verificationId = c.VerificationID, // Use correct casing for frontend
					ClaimId = c.Claim.ClaimId,
					FullName = $"{c.Claim.Lecturer.User.FirstName} {c.Claim.Lecturer.User.LastName}",
					VerificationDate = c.VerificationDate,
					claimStatus = c.VerificationStatus
				})
				.ToList();

			return Json(claimVerifications); // Return the verified claims as JSON
		}

		[HttpGet("Approval/GetVerifiedClaimDetails/{verificationId}")]
		public IActionResult GetVerifiedClaimDetails(int verificationId)
		{
			var claimVerification = _context.ClaimVerifications
				.Include(cv => cv.Claim)
				.ThenInclude(c => c.Lecturer)
				.ThenInclude(l => l.User) // Include User information related to the Lecturer
				.Include(cv => cv.Coordinator)
				.ThenInclude(co => co.User) // Include User information related to the Coordinator
				.FirstOrDefault(cv => cv.VerificationID == verificationId); // Find claim by VerificationID

			if (claimVerification == null)
			{
				return NotFound(); // Return 404 if the claim does not exist
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
				TotalPay = (claimVerification.Claim.HoursWorked * claimVerification.Claim.Lecturer.HourlyRate) +
						   (claimVerification.Claim.OvertimeHoursWorked * claimVerification.Claim.Lecturer.HourlyRate * 1.5M),
				CoordinatorId = claimVerification.Coordinator.CoordinatorID,
				CoordinatorUserName = claimVerification.Coordinator.User.UserName,
				CoordinatorFullName = $"{claimVerification.Coordinator.User.FirstName} {claimVerification.Coordinator.User.LastName}",
				CoordinatorDepartment = claimVerification.Coordinator.Department,
				CoordinatorCampus = claimVerification.Coordinator.Campus,
				VerificationID = verificationId
			};

			return Json(claimDetails); // Return the claim details as JSON

		}

		[HttpPost("Approval/ApproveClaim/{verificationId}")]
		public IActionResult ApproveClaim(int verificationId)
		{
			// Retrieve the verified claim using the verificationId
			var claimVerification = _context.ClaimVerifications
				.Include(cv => cv.Claim) // Include the Claim to get necessary details
				.FirstOrDefault(cv => cv.VerificationID == verificationId); // Find claim verification by VerificationID

			if (claimVerification == null)
			{
				return NotFound(); // Return 404 if the claim verification does not exist
			}

			var academicManagerID = HttpContext.Session.GetInt32("AcademicManagerID");
			if (academicManagerID == null)
			{
				return NotFound(); // Return 404 if the coordinator ID is not found in the session
			}

			// Create a new ClaimApproval entry
			var claimApproval = new ClaimApproval
			{
				ClaimID = claimVerification.Claim.ClaimId, // Use the ClaimID from the verified claim
				ManagerID = (int)academicManagerID, // Assuming the coordinator is also the manager for this claim
				ApprovalDate = DateTime.UtcNow, // Set the current date and time
				IsApproved = true, // Indicate that the claim is approved
				ApprovalStatus = "Approved", // Set the approval status
				ApprovalComments = "Claim approved successfully." // Optional comments about the approval
			};

			// Add the new approval entry to the context
			_context.ClaimApprovals.Add(claimApproval);

			// Update the original claim status to accepted
			claimVerification.VerificationStatus = "Approved"; // Get the original claim from the verified claim

			var claim = claimVerification.Claim; // Get the original claim from the verified claim
			claim.Status = "Approved"; // Update status for rejected claims

			// Save changes to the database
			_context.SaveChanges();
			return RedirectToAction("Approval"); // Redirect to the Verification page
		}


		[HttpPost("Approval/RejectVerifiedClaim/{verificationId}")]
		public IActionResult RejectVerifiedClaim(int verificationId)
		{
			// Retrieve the verified claim using the verificationId
			var claimVerification = _context.ClaimVerifications
				.Include(cv => cv.Claim) // Include the Claim to get necessary details
				.FirstOrDefault(cv => cv.VerificationID == verificationId); // Find claim verification by VerificationID

			if (claimVerification == null)
			{
				return NotFound(); // Return 404 if the claim verification does not exist
			}

			var academicManagerID = HttpContext.Session.GetInt32("AcademicManagerID");
			if (academicManagerID == null)
			{
				return NotFound(); // Return 404 if the academic manager ID is not found in the session
			}

			claimVerification.VerificationStatus = "Approved"; 

			// Update the original claim status to rejected
			var claim = claimVerification.Claim; // Get the original claim from the verified claim
			claim.Status = "Rejected"; // Update status for rejected claims

			// Create a new ClaimApproval entry for the rejection
			var claimApproval = new ClaimApproval
			{
				ClaimID = claim.ClaimId, // Use the ClaimID from the verified claim
				ManagerID = (int)academicManagerID, // Use the academic manager ID from the session
				ApprovalDate = DateTime.UtcNow, // Set the current date and time
				IsApproved = false, // Indicate that the claim is rejected
				ApprovalStatus = "Rejected", // Set the approval status
				ApprovalComments = "Claim rejected due to [reason]." // Optional comments about the rejection
			};

			// Add the new approval entry to the context
			_context.ClaimApprovals.Add(claimApproval);

			// Save changes to the database
			_context.SaveChanges(); // Ensure changes are saved

			return RedirectToAction("Approval"); // Redirect to the Approval page
		}
	}
}
