
using Microsoft.EntityFrameworkCore;

namespace HashingAndSalting.Models
{
	public class AppDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HashingAndSaltingDb;Integrated Security=True;");
		}

		public DbSet<User>? Users { get; set; } 
	}
}
