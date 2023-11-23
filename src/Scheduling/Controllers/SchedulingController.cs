using Microsoft.AspNetCore.Mvc;
using Scheduling.Services;

namespace Scheduling.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SchedulingController : Controller
	{
		private readonly ISchedulingService schedulingService;

		public SchedulingController(ISchedulingService schedulingService)
		{
			this.schedulingService = schedulingService;
		}

		[HttpGet]
		public async Task<IActionResult> TestApi([FromQuery] DateTime date)
		{
			return Ok(await schedulingService.GetExchangeRate(date));
		}
	}
}
