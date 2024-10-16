using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class User
	{
		[Key]
		public int UserID
		{
			get; set;
		}  // Unique User ID

		[Required]
		public string FirstName
		{
			get; set;
		}

		[Required]
		public string LastName
		{
			get; set;
		}

		[Required]
		public string UserName
		{
			get; set;
		}

		[Required]
		public string Password
		{
			get; set;
		}

		public string? ContactInfo
		{
			get; set;
		}

		// Method to update contact information
		public void UpdateContactInformation(string newContactInfo)
		{
			ContactInfo = newContactInfo;
		}
	}
}
