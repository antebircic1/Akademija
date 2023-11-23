using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries
{
	public class GetTestRecordsQuery : IRequest<List<TestDto>>
	{
		public class GetTestRecordsQueryHandler : IRequestHandler<GetTestRecordsQuery, List<TestDto>>
		{
			private readonly IAcademyDbContext context;
			private readonly ILogger<GetTestRecordsQuery> logger;
			private readonly IMapper mapper;

			public GetTestRecordsQueryHandler(IAcademyDbContext context, ILogger<GetTestRecordsQuery> logger, IMapper mapper)
			{
				this.context = context;
				this.logger = logger;
				this.mapper = mapper;
			}

			public async Task<List<TestDto>> Handle(GetTestRecordsQuery request, CancellationToken cancellationToken)
			{
				this.logger.LogInformation("Getting test query.");

				var test = await this.context.Tests.ToListAsync(cancellationToken: cancellationToken);

				var testDto = this.mapper.Map<List<TestDto>>(test);

				return testDto;
			}
		}
	}
}
