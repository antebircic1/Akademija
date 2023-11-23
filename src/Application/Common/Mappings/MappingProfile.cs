using Application.Common.Dtos;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Common.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Test, TestDto>()
				.ReverseMap();
		}
	}
}
