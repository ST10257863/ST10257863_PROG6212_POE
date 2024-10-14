using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class Coordinator
	{
		[Key]
		public int CoordinatorId
		{
			get; set;
		}

		public string department
		{
			get; set;
		} = string.Empty;

		public string campus
		{
			get; set;
		} = string.Empty;

		public ICollection<ClaimVerification> claimVerifications { get; set; } = null!;
	}
}
