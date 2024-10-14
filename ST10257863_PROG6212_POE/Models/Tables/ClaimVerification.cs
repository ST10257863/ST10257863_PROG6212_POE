using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class ClaimVerification
	{
		[Key]
		public int VerificationID
		{
			get; set;
		}

		[ForeignKey("Claim")]
		public int ClaimID
		{
			get; set;
		}
		public Claim Claim { get; set; } = null!;

		[ForeignKey("Coordinator")]
		public int CoordinatorID
		{
			get; set;
		}
		public Coordinator Coordinator { get; set; } = null!;

		public DateTime VerificationDate
		{
			get; set;
		}
		public string VerificationStatus
		{
			get; set;
		} // Verified, Rejected
		public bool IsVerified
		{
			get; set;
		}
		public string VerificationComments
		{
			get; set;
		}
	}
}
