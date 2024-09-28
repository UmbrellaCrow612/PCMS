using AutoMapper;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<POSTReport, Report>();
            CreateMap<Report, GETReport>();
            CreateMap<PATCHReport, Report>();
        }
    }
}