using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class ClaimApproval
	{
		[Key]
		public int ApprovalId
		{
			get; set;
		}

		public int ManagerId
		{
			get; set;
		}

		public int ClaimId
		{
			get; set;
		}

		public AcademicManager academicManager { get; set; } = null!;

		public DateTime ApprovalDate
		{
			get; set;
		}

		public string ApprovalStatus
		{
			get; set;
		} = string.Empty;

		public Boolean IsApproved
		{
			get; set;
		}

		public string ApprovalComments
		{
			get; set;
		} = string.Empty;

		public ICollection<Claim> Claims { get; set; } = null!;
	}
}