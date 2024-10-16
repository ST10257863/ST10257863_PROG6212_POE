using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Models.Tables;

namespace ST10257863_PROG6212_POE.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		// DbSets
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
			// User entity
			modelBuilder.Entity<User>().HasKey(u => u.UserID);

			// Lecturer entity with foreign key relationship to User
			modelBuilder.Entity<Lecturer>()
				.HasOne(l => l.User)
				.WithMany() // No navigation back from User to Lecturer
				.HasForeignKey(l => l.UserID)
				.OnDelete(DeleteBehavior.Restrict); // No cascading delete

			// Coordinator entity with foreign key relationship to User
			modelBuilder.Entity<Coordinator>()
				.HasOne(c => c.User)
				.WithMany() // No navigation back from User to Coordinator
				.HasForeignKey(c => c.UserID)
				.OnDelete(DeleteBehavior.Restrict);

			// AcademicManager entity with foreign key relationship to User
			modelBuilder.Entity<AcademicManager>()
				.HasOne(am => am.User)
				.WithOne()
				.HasForeignKey<AcademicManager>(am => am.UserID)
				.OnDelete(DeleteBehavior.Restrict);



			// Seed User Data
			modelBuilder.Entity<User>().HasData(
				new User { UserID = 1, FirstName = "Admin", LastName = "User", UserName = "admin", Password = "adminPass123", ContactInfo = "admin@example.com" },
				new User { UserID = 2, FirstName = "John", LastName = "Doe", UserName = "lecturerUser", Password = "LecturerPass123", ContactInfo = "lecturer@example.com" },
				new User { UserID = 3, FirstName = "Jane", LastName = "Smith", UserName = "coordinatorUser", Password = "CoordinatorPass123", ContactInfo = "coordinator@example.com" },
				new User { UserID = 4, FirstName = "Mike", LastName = "Johnson", UserName = "managerUser", Password = "ManagerPass123", ContactInfo = "manager@example.com" }
			);

			// Seed Lecturer Data
			modelBuilder.Entity<Lecturer>().HasData(
				new Lecturer { LecturerID = 1001, UserID = 1, HourlyRate = 500.00m, Department = "Computer Science", Campus = "Main Campus" },
				new Lecturer { LecturerID = 1002, UserID = 2, HourlyRate = 450.00m, Department = "Mathematics", Campus = "South Campus" }
			);

			// Seed Coordinator Data
			modelBuilder.Entity<Coordinator>().HasData(
				new Coordinator { CoordinatorID = 2001, UserID = 1, Department = "Engineering", Campus = "North Campus" }, // Admin as Coordinator
				new Coordinator { CoordinatorID = 2002, UserID = 3, Department = "Science", Campus = "West Campus" } // Coordinator user
			);

			// Seed Academic Manager Data
			modelBuilder.Entity<AcademicManager>().HasData(
				new AcademicManager { ManagerID = 3001, UserID = 1, Department = "IT", Campus = "Main Campus" }, // Admin as Academic Manager
				new AcademicManager { ManagerID = 3002, UserID = 4, Department = "Business", Campus = "East Campus" } // Manager-only user
			);

		}
	}
}
