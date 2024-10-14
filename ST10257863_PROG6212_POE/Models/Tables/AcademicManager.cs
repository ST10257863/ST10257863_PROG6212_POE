using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class AcademicManager
	{
		[Key]
		public int ManagerID
		{
			get; set;
		}

		[ForeignKey("User")]
		public int UserID
		{
			get; set;
		}
		public User User { get; set; } = null!;

		public string Department
		{
			get; set;
		}
		public string Campus
		{
			get; set;
		}

		// Methods to approve and reject claims (specific to AcademicManager)
		public void ApproveClaim(ClaimApproval approval)
		{
			approval.ApprovalStatus = "Approved";
			approval.IsApproved = true;
		}

		public void RejectClaim(ClaimApproval approval)
		{
			approval.ApprovalStatus = "Rejected";
			approval.IsApproved = false;
		}
	}
}
