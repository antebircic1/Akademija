using Application.Common.Interfaces;
using Domain.Entities;
using Scheduling.Common.Entities;
using System.Text.Json;

namespace Scheduling.Services
{
	public class SchedulingService : ISchedulingService
	{
		static readonly HttpClient httpClient = new();
		private readonly IAcademyDbContext context;
		private DateTime startDate;
        public SchedulingService(IAcademyDbContext context)
        {
			this.context = context;
		}
        public async Task<List<ExchangeRateViewModel>> GetExchangeRate(DateTime date)
		{
			return await GetExchangeRateFromApi(date);
		}
		#region Private

		private async Task<List<ExchangeRateViewModel>> GetExchangeRateFromApi(DateTime date)
		{
			try
			{
				if(startDate == DateTime.MinValue)
				{
					startDate = date;
				}
				var req = new HttpRequestMessage()
				{
					RequestUri = new Uri($"https://api.hnb.hr/tecajn-eur/v3?datum-primjene={startDate.Date.ToString("yyyy-MM-dd")}"),
					Method = HttpMethod.Get
				};
				HttpResponseMessage response = await httpClient.SendAsync(req);
				var responseBody = await response.Content.ReadAsStringAsync();

				var exchangeRate = JsonSerializer.Deserialize<List<ExchangeRateViewModel>>(responseBody);
				
				var list = exchangeRate.Select(x=> new ExchangeRate()
				{
					Id = Guid.NewGuid(),
					ImeValute = x.valuta,
					KupovniTecaj = Decimal.Parse(x.kupovni_tecaj),
					SrednjiTecaj = Decimal.Parse(x.srednji_tecaj),
					ProdajniTecaj = Decimal.Parse(x.prodajni_tecaj),
					DatumUnosa = date
				}).ToList();

				await SaveExchangeRates(list, new CancellationToken());
				startDate = date.AddDays(1);
				return exchangeRate;

			}
			catch (Exception e)
			{
				throw e;
			}
		}

		private async Task SaveExchangeRates(List<ExchangeRate> exchangeRates, CancellationToken cancellationToken)
		{
			await this.context.ExchangeRates.AddRangeAsync(exchangeRates, cancellationToken);

			await this.context.SaveChangesAsync(cancellationToken);
		}

		#endregion

	}
}
