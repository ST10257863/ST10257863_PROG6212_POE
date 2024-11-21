using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10257863_PROG6212_POE.Models.Tables
{
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
		public DateTime SubmissionDate
		{
			get; set;
		}

		[Required]
		public string Status
		{
			get; set;
		} = "Pending"; // Default: Pending		Verified, Approved, Rejected

		public string? LecturerNotes
		{
			get; set;
		}

		public List<string> SupportingDocuments { get; set; } = new List<string>();


		//----------------------------------------Approval----------------------------------------
		// Combined fields for approval information
		[ForeignKey("AcademicManager")]
		public int? ManagerId
		{
			get; set;
		}  // Nullable in case not yet approved
		public AcademicManager? Manager
		{
			get; set;
		}

		public DateTime? ApprovalDate
		{
			get; set;
		}  // Nullable in case not yet approved
		public bool? IsApproved
		{
			get; set;
		}// Nullable to indicate if approval is pending
		public string? ApprovalComments
		{
			get; set;
		}


		//----------------------------------------Verification----------------------------------------
		// Combined fields for verification information
		[ForeignKey("Coordinator")]
		public int? CoordinatorId
		{
			get; set;
		}  // Nullable in case not yet verified
		public Coordinator? Coordinator
		{
			get; set;
		}

		public DateTime? VerificationDate
		{
			get; set;
		}  // Nullable in case not yet verified
		public bool? IsVerified
		{
			get; set;
		}// Nullable to indicate if verification is pending
		public string? VerificationComments
		{
			get; set;
		}
	}
}
