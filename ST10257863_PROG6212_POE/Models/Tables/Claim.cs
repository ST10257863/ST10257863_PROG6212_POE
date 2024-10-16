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

		[NotMapped] // Calculated field
		public decimal ClaimAmount
		{
			get
			{
				return CalculateClaimAmount();
			}
			set
			{
			} // EF requires this
		}

		public DateTime SubmissionDate
		{
			get; set;
		}

		[Required]
		public string Status
		{
			get; set;
		} // Pending, Approved, Rejected

		public List<string> SupportingDocuments { get; set; } = new List<string>();

		// Method to calculate claim amount
		public decimal CalculateClaimAmount()
		{
			return HoursWorked * Lecturer.HourlyRate;
		}
	}
}
