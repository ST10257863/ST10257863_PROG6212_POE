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

		public Lecturer Lecturer { get; set; } = null!;

		[Range(0, 1000, ErrorMessage = "Hours worked must be a positive number.")]
		public decimal HoursWorked
		{
			get; set;
		}

		[NotMapped] // Calculated field, not stored in DB
		public decimal ClaimAmount
		{
			get
			{
				return CalculateClaimAmount();
			}
			set
			{
			} // To satisfy EF requirements, even though it's calculated.
		}

		[DataType(DataType.DateTime)]
		public DateTime SubmissionDate
		{
			get; set;
		}

		[Required]
		[StringLength(50)]
		public string Status
		{
			get; set;
		}

		// Method to calculate the claim amount dynamically
		public decimal CalculateClaimAmount()
		{
			return HoursWorked * Lecturer.HourlyRate;
		}
	}
}
