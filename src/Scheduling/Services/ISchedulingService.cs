using Scheduling.Common.Entities;

namespace Scheduling.Services
{
	public interface ISchedulingService
	{
		Task<List<ExchangeRate>> GetExchangeRate(DateTime date);
	}
}
