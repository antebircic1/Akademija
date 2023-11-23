using Scheduling.Common.Entities;
using System.Text.Json;

namespace Scheduling.Services
{
	public class SchedulingService : ISchedulingService
	{
		static readonly HttpClient httpClient = new();

		public async Task<List<ExchangeRate>> GetExchangeRate(DateTime date)
		{
			return await GetExchangeRateFromApi(date);
		}

		private async Task<List<ExchangeRate>> GetExchangeRateFromApi(DateTime date)
		{
			try
			{
				var req = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://api.hnb.hr/tecajn-eur/v3?datum-primjene={date.Date.ToString("yyyy-MM-dd")}"),
					Method = HttpMethod.Get
				};
				HttpResponseMessage response = await httpClient.SendAsync(req);
				var responseBody = await response.Content.ReadAsStringAsync();

				List<ExchangeRate> exchangeRate = JsonSerializer.Deserialize<List<ExchangeRate>>(responseBody);

				return exchangeRate;

			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
