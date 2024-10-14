using Microsoft.EntityFrameworkCore;
using ST10257863_PROG6212_POE.Models.Tables;

namespace ST10257863_PROG6212_POE.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		DbSet<User> User
		{
			get; set;
		}

		DbSet<>
	}
}
