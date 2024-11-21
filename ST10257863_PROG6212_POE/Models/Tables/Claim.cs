using ST10257863_PROG6212_POE.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Claim
{
	[Key]
	public int ClaimId
	{
		get; set;
	}

	[ForeignKey("Lecturer")]
	public int LecturerId
	{
		get; set;
	}
	public Lecturer Lecturer
	{
		get; set;
	}

	public decimal HoursWorked
	{
		get; set;
	}
	public decimal OvertimeHoursWorked
	{
		get; set;
	}
	public decimal HourlyRate
	{
		get; set;
	}
	public DateTime SubmissionDate
	{
		get; set;
	}

	[Required]
	public string Status { get; set; } = "Pending";

	public string? LecturerNotes
	{
		get; set;
	}

	public List<string> SupportingDocuments { get; set; } = new List<string>();

	//----------------------------------------Approval----------------------------------------
	[ForeignKey("AcademicManager")]
	public int? ManagerId
	{
		get; set;
	}
	public AcademicManager? Manager
	{
		get; set;
	}

	public DateTime? ApprovalDate
	{
		get; set;
	}
	public bool? IsApproved
	{
		get; set;
	}
	public string? ApprovalComments
	{
		get; set;
	}

	//----------------------------------------Verification----------------------------------------
	[ForeignKey("Coordinator")]
	public int? CoordinatorId
	{
		get; set;
	}
	public Coordinator? Coordinator
	{
		get; set;
	}

	public DateTime? VerificationDate
	{
		get; set;
	}
	public bool? IsVerified
	{
		get; set;
	}
	public string? VerificationComments
	{
		get; set;
	}

	// Navigation property for linked files
	public ICollection<ClaimFile> ClaimFiles { get; set; } = new List<ClaimFile>();
}
