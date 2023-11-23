using Scheduling.Common.Entities;

namespace Scheduling.Services
{
	public interface ISchedulingService
	{
		Task<List<ExchangeRateViewModel>> GetExchangeRate(DateTime date);
	}
}
