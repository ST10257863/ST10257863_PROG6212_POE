using System.ComponentModel.DataAnnotations;

namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class User
	{
		[Key]
		public int Id
		{
			get; set;
		}

		public string FirstName
		{
			get; set;
		}

		public string LastName
		{
			get; set;
		}

		public string UserName
		{
			get; set;
		}

		public string Password
		{
			get; set;
		}

		public string ContactInfo
		{
			get; set;
		}
	}
}