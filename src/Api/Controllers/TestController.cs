using Api.Common;
using Api.Common.Validations;
using Application.Commands;
using Application.Common.Dtos;
using Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class TestController : BaseApiController
	{
		[HttpGet]
		public async Task<IActionResult> GetTest()
		{
			return Ok(await Mediator.Send(new GetTestRecordsQuery()));
		}

		[HttpPost]
		public async Task<IActionResult> PostTest([FromBody] TestDto testDto)
		{
			var testDtoValidator = new TestDtoValidator();

			var validatorResult = testDtoValidator.Validate(testDto);

			if (validatorResult.IsValid)
			{
				return Ok(await Mediator.Send(new InsertTestRecordCommand { TestDto = testDto }));
			}

			var errorMessages = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
			return BadRequest(errorMessages);
		}
	}
}
