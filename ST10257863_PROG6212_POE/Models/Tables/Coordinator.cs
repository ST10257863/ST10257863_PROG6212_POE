using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class Coordinator
	{
		//[Key]
		public int CoordinatorID
		{
			get; set;
		}

		[ForeignKey("User")]
		public int UserID
		{
			get; set;
		}  // Foreign key from User

		public User User
		{
			get; set;
		}  // Navigation property to User

		public string Department
		{
			get; set;
		}
		public string Campus
		{
			get; set;
		}

		// Methods to verify and reject claims (specific to Coordinator)
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
