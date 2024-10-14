using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class ClaimApproval
	{
		[Key]
		public int ApprovalID
		{
			get; set;
		}

		[ForeignKey("Claim")]
		public int ClaimID
		{
			get; set;
		}
		public Claim Claim { get; set; } = null!;

		[ForeignKey("AcademicManager")]
		public int ManagerID
		{
			get; set;
		}
		public AcademicManager Manager { get; set; } = null!;

		public DateTime ApprovalDate
		{
			get; set;
		}
		public bool IsApproved
		{
			get; set;
		}
		public string ApprovalStatus
		{
			get; set;
		} // Approved, Rejected
		public string ApprovalComments
		{
			get; set;
		}
	}
}
