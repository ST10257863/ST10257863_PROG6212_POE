using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class Lecturer
	{
		[Key]
		public int LecturerID
		{
			get; set;
		}

		[ForeignKey("User")]
		public int UserID
		{
			get; set;
		}
		public User User { get; set; } = null!;

		public decimal HourlyRate
		{
			get; set;
		}

		public string Department
		{
			get; set;
		}

		public string Campus
		{
			get; set;
		}

		// Method to submit a claim (specific to Lecturer)
		public void SubmitClaim(Claim claim)
		{
			// Logic to submit a claim
		}
	}
}
