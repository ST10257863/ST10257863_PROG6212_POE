using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Data;
using ST10257863_PROG6212_POE.Models.Tables;
using System.Security.Claims;

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

		//Approves a verified claim, updates the claim status, and creates an entry in the ClaimApproval table
		[HttpPost("Approval/ApproveClaim/{verificationId}")]
		public IActionResult ApproveClaim(int claimId)
		{
			var claim = _context.Claims
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return RedirectToAction("Approval");
			}

			var academicManagerID = HttpContext.Session.GetInt32("AcademicManagerID");
			if (academicManagerID == null)
			{
				return RedirectToAction("Approval");
			}

			claim.ManagerId = (int)academicManagerID;
			claim.ApprovalDate = DateTime.UtcNow;
			claim.IsApproved = true;
			claim.ApprovalComments = "";
			claim.Status = "Approved";

			_context.SaveChanges();
			return RedirectToAction("Approval");
		}

		//Rejects a verified claim, updates the claim status, and creates an entry in the ClaimApproval table for the rejection.
		[HttpPost("Approval/RejectVerifiedClaim/{verificationId}")]
		public IActionResult RejectClaim(int claimId)
		{

			var claim = _context.Claims
				.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return RedirectToAction("Approval");
			}

			var academicManagerID = HttpContext.Session.GetInt32("AcademicManagerID");
			if (academicManagerID == null)
			{
				return RedirectToAction("Approval");
			}

			claim.ManagerId = (int)academicManagerID;
			claim.ApprovalDate = DateTime.UtcNow;
			claim.IsApproved = true;
			claim.ApprovalComments = "";
			claim.Status = "Rejected";

			_context.SaveChanges();
			return RedirectToAction("Approval");
		}
	}
}
