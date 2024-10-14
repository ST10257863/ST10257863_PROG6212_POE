using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class AcademicManager
	{
		[Key]
		public int Id
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

		public ICollection<ClaimApproval> ClaimApprovals
		{
			get; set;
		} = null!;
	}
}
