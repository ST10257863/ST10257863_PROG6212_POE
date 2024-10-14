using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class Lecturer
	{
		[Key]
		public int LecturerID
		{
			get; set;
		}

		[Required]
		public decimal HourlyRate
		{
			get; set;
		}

		[Required]
		public string Department
		{
			get; set;
		}

		[Required]
		public string Campus
		{
			get; set;
		}

		// Method for submitting a claim (implementation later)
		public void SubmitClaim(Claim claim)
		{
			// Claim submission logic
		}
	}
}
