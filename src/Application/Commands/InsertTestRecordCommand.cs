using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands
{
	public class InsertTestRecordCommand : IRequest<TestDto>
	{
		public TestDto TestDto { get; set; }

		public class InsertTestRecordCommandHandler : IRequestHandler<InsertTestRecordCommand, TestDto>
		{
			private readonly IAcademyDbContext context;
			private readonly ILogger<GetTestRecordsQuery> logger;
			private readonly IMapper mapper;

			public InsertTestRecordCommandHandler(IAcademyDbContext context, ILogger<GetTestRecordsQuery> logger, IMapper mapper)
			{
				this.context = context;
				this.logger = logger;
				this.mapper = mapper;
			}


			public async Task<TestDto> Handle(InsertTestRecordCommand request, CancellationToken cancellationToken)
			{
				this.logger.LogInformation("Inserting test record!");

				try
				{
					var test = this.mapper.Map<Test>(request.TestDto);

					await this.context.Tests.AddAsync(test, cancellationToken);

					await this.context.SaveChangesAsync(cancellationToken);

					var testDto = this.mapper.Map<TestDto>(test);

					return testDto;
				}
				catch (Exception e)
				{
					this.logger.LogError($"Error inserting test record: {e}");
					throw;
				}

			}
		}
	}
}
