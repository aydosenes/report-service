using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class BaseMapper : Profile
    {
        public BaseMapper()
        {
            CreateMap<Report, ReportDto>().ReverseMap();
        }
    }
}
