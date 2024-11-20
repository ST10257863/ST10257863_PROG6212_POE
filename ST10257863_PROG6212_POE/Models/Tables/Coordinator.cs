using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class Coordinator
	{
		[Key]
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

		//// Verify the claim and update its verification status
		//public void VerifyClaim(Claim claim)
		//{
		//	// Check if claim is already verified
		//	if (claim.IsVerified.HasValue && claim.IsVerified.Value)
		//	{
		//		throw new InvalidOperationException("Claim already verified.");
		//	}
		//	claim.Status = "Verified";
		//	claim.IsVerified = true;
		//	claim.VerificationDate = DateTime.Now;  // Set verification date
		//}
		//// Reject the claim and update its rejection status
		//public void RejectClaim(Claim claim, string comments)
		//{
		//	// Check if claim is already rejected
		//	if (claim.IsVerified.HasValue && !claim.IsVerified.Value)
		//	{
		//		throw new InvalidOperationException("Claim already rejected.");
		//	}
		//	claim.Status = "Rejected";
		//	claim.IsVerified = false;
		//	claim.VerificationComments = comments;
		//	claim.VerificationDate = DateTime.Now;  // Set rejection date
		//}
	}
}
