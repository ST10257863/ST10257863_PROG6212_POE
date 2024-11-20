using ST10257863_PROG6212_POE.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class AcademicManager
{
	[Key]
	public int ManagerID
	{
		get; set;
	}  // Unique AcademicManager ID

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

	//// Approve the claim and update its approval status
	//public void ApproveClaim(Claim claim)
	//{
	//	// Check if claim is not already approved
	//	if (claim.IsApproved.HasValue && claim.IsApproved.Value)
	//	{
	//		throw new InvalidOperationException("Claim already approved.");
	//	}
	//	claim.Status = "Approved";
	//	claim.IsApproved = true;
	//	claim.ApprovalDate = DateTime.Now;  // Set approval date
	//}
	//// Reject the claim and update its rejection status
	//public void RejectClaim(Claim claim, string comments)
	//{
	//	// Check if claim is already rejected
	//	if (claim.IsApproved.HasValue && !claim.IsApproved.Value)
	//	{
	//		throw new InvalidOperationException("Claim already rejected.");
	//	}
	//	claim.Status = "Rejected";
	//	claim.IsApproved = false;
	//	claim.ApprovalComments = comments;
	//	claim.ApprovalDate = DateTime.Now;  // Set rejection date
	//}
}
