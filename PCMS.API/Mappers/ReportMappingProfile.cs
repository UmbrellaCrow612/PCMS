using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.PATCH;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<CreateReportDto, Report>();
            CreateMap<Report, ReportDto>();
            CreateMap<UpdateReportDto, Report>();
        }
    }
}