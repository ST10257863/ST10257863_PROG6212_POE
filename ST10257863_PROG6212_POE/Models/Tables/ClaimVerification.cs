using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class ClaimVerification
	{

		[Key]
		public int VerificationId
		{
			get; set;
		}

		public int CoordinatorID
		{
			get; set;
		}
		public int ClaimId
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
		} = string.Empty;

		public Boolean IsVerified
		{
			get; set;
		}

		public string VerificationComments
		{
			get; set;
		} = string.Empty;

		public ICollection<Claim> Claims { get; set; } = null!;
	}
}