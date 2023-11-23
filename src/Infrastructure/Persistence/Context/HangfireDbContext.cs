using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
	public class HangfireDbContext : DbContext
	{
		public HangfireDbContext(DbContextOptions<HangfireDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public void EnsureDatabaseCreated()
		{
			Database.EnsureCreated();
		}
	}
}
