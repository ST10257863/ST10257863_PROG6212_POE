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
