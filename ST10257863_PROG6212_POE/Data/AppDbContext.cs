using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Models.Tables;

namespace ST10257863_PROG6212_POE.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users
		{
			get; set;
		}
		public DbSet<Lecturer> Lecturers
		{
			get; set;
		}
		public DbSet<Coordinator> Coordinators
		{
			get; set;
		}
		public DbSet<AcademicManager> AcademicManagers
		{
			get; set;
		}
		public DbSet<Claim> Claims
		{
			get; set;
		}
		public DbSet<ClaimVerification> ClaimVerifications
		{
			get; set;
		}
		public DbSet<ClaimApproval> ClaimApprovals
		{
			get; set;
		}
	}
}
