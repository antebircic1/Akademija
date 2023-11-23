using Application.Common.Dtos;
using FluentValidation;

namespace Api.Common.Validations
{
	public class TestDtoValidator : AbstractValidator<TestDto>
	{
		public TestDtoValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty()
				.WithMessage("Name is required");
		}
	}
}
