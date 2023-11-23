using Hangfire;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Scheduling.Services;

namespace Scheduling
{
	public static class ServiceConfiguration
	{
		public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContextFactory<HangfireDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("HangfireConnection")));

			var serviceProvider = services.BuildServiceProvider();
			using (var scope = serviceProvider.CreateScope())
			{
				var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<HangfireDbContext>>();
				using (var dbContext = dbContextFactory.CreateDbContext())
				{
					dbContext.EnsureDatabaseCreated();
				}
			}

			GlobalConfiguration.Configuration
				.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));

			services.AddHangfire(cfg => cfg
				.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
				.UseSimpleAssemblyNameTypeSerializer()
				.UseRecommendedSerializerSettings()
				.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

			services.AddSingleton<ISchedulingService, SchedulingService>();

			services.AddHangfireServer();
		}
	}
}
