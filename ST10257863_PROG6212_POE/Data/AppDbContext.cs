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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed Users
			modelBuilder.Entity<User>().HasData(
				new User { UserID = 1, FirstName = "Admin", LastName = "User", UserName = "admin", Password = "adminPass123", ContactInfo = "admin@example.com" },
				new User { UserID = 2, FirstName = "John", LastName = "Doe", UserName = "lecturerUser", Password = "LecturerPass123", ContactInfo = "lecturer@example.com" },
				new User { UserID = 3, FirstName = "Jane", LastName = "Smith", UserName = "coordinatorUser", Password = "CoordinatorPass123", ContactInfo = "coordinator@example.com" },
				new User { UserID = 4, FirstName = "Mike", LastName = "Johnson", UserName = "managerUser", Password = "ManagerPass123", ContactInfo = "manager@example.com" }
			);

			// Seed Lecturers
			modelBuilder.Entity<Lecturer>().HasData(
				new Lecturer { LecturerID = 1, UserID = 1, HourlyRate = 500.00m, Department = "Computer Science", Campus = "Main Campus" }, // Admin as Lecturer
				new Lecturer { LecturerID = 2, UserID = 2, HourlyRate = 450.00m, Department = "Math", Campus = "South Campus" } // Lecturer-only user
			);

			// Seed Coordinators
			modelBuilder.Entity<Coordinator>().HasData(
				new Coordinator { CoordinatorID = 1, UserID = 1, Department = "Engineering", Campus = "North Campus" }, // Admin as Coordinator
				new Coordinator { CoordinatorID = 2, UserID = 3, Department = "Science", Campus = "West Campus" } // Coordinator-only user
			);

			// Seed Academic Managers
			modelBuilder.Entity<AcademicManager>().HasData(
				new AcademicManager { ManagerID = 1, UserID = 1, Department = "IT", Campus = "Main Campus" }, // Admin as Academic Manager
				new AcademicManager { ManagerID = 2, UserID = 4, Department = "Business", Campus = "East Campus" } // Manager-only user
			);
		}
	}
}
