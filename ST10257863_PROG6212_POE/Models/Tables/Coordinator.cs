using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class Coordinator
	{
		[Key]
		public int CoordinatorID
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

		// Methods for verifying and rejecting claims
		public void VerifyClaim(ClaimVerification verification)
		{
			verification.VerificationStatus = "Verified";
			verification.IsVerified = true;
		}

		public void RejectClaim(ClaimVerification verification)
		{
			verification.VerificationStatus = "Rejected";
			verification.IsVerified = false;
		}
	}
}
