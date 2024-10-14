using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class AcademicManager
	{
		[Key]
		public int ManagerID
		{
			get; set;
		}

		[Required]
		public string Department
		{
			get; set;
		}

		[Required]
		public string Campus
		{
			get; set;
		}

		// Methods for approving and rejecting claims
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
