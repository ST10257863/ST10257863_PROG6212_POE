using Microsoft.AspNetCore.Mvc;
using ST10257863_PROG6212_POE.Data;

namespace ST10257863_PROG6212_POE.Controllers
{
	public class VerificationController : Controller
	{
		private readonly AppDbContext _context;

		public VerificationController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Verification()
		{
			return View();
		}

		// Accepts a claim, updates its status to "Verified," and records the verification in the database.
		[HttpPost]
		public IActionResult VerifyClaim(int claimId, string verificationComment)
		{
			var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return RedirectToAction("Verification");
			}

			var coordinatorId = HttpContext.Session.GetInt32("CoordinatorID");

			if (coordinatorId == null)
			{
				return RedirectToAction("Verification");
			}

			claim.CoordinatorId = coordinatorId;
			claim.VerificationDate = DateTime.UtcNow;
			claim.IsVerified = true;
			claim.VerificationComments = verificationComment; // Save the provided comment
			claim.Status = "Verified";

			_context.SaveChanges();
			return RedirectToAction("Verification");
		}

		// Rejects a claim and updates its status to "Rejected."
		[HttpPost]
		public IActionResult RejectClaim(int claimId, string verificationComment)
		{
			var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == claimId);

			if (claim == null)
			{
				return RedirectToAction("Verification");
			}

			var coordinatorId = HttpContext.Session.GetInt32("CoordinatorID");
			if (coordinatorId == null)
			{
				return RedirectToAction("Verification");
			}

			claim.CoordinatorId = coordinatorId;
			claim.VerificationDate = DateTime.UtcNow;
			claim.IsVerified = true;
			claim.VerificationComments = verificationComment; // Save the provided comment
			claim.Status = "Rejected";

			_context.SaveChanges();
			return RedirectToAction("Verification");
		}
	}
}
