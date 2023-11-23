using Application.Common.Interfaces;
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

			services.AddDbContext<AcademyDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly(typeof(AcademyDbContext).Assembly.FullName)));

			services.AddScoped<IAcademyDbContext>(provider => provider.GetService<AcademyDbContext>());

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

			services.AddScoped<ISchedulingService, SchedulingService>();

			services.AddHangfireServer();

			var schedulingService = services.BuildServiceProvider().GetService<ISchedulingService>();
			try
			{
				var date = new DateTime(2023, 11, 01, 00, 00, 0);
				RecurringJob.AddOrUpdate("Exchange Rate", () => schedulingService.GetExchangeRate(date),
				Cron.Minutely);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
	}
}
