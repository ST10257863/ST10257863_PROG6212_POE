using ST10257863_PROG6212_POE.Models.Tables;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Lecturer
{
	[Key]
	public int LecturerID
	{
		get; set;
	}  // Unique Lecturer ID

	[ForeignKey("User")]
	public int UserID
	{
		get; set;
	}  // Foreign key from User

	public User User
	{
		get; set;
	}  // Navigation property to User

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

	public void SubmitClaim(Claim claim)
	{
		// Logic for submitting a claim
	}
}
