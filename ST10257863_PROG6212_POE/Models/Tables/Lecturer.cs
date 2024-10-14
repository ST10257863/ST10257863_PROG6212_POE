using System.ComponentModel.DataAnnotations;


namespace ST10257863_PROG6212_POE.Models.Tables
{
	public class Lecturer
	{
		[Key]
		public int Id
		{
			get; set;
		}

		public decimal HourlyRate
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
	}
}
