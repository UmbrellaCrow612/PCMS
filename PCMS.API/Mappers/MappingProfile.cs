using AutoMapper;
using PCMS.API.DTOS;
using PCMS.API.Models;

namespace PCMS.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Cases
            CreateMap<Case, POSTCase>();
            CreateMap<POSTCase, Case>();
            CreateMap<Case, GETCase>();
            CreateMap<GETCase, Case>();
            CreateMap<Case, PATCHCase>();
            CreateMap<PATCHCase, Case>();

            // Users
            CreateMap<ApplicationUser, GETApplicationUser>();
            CreateMap<GETApplicationUser, ApplicationUser>();

            // Case action
            CreateMap<CaseAction, POSTCaseAction>();
            CreateMap<POSTCaseAction, CaseAction>();
            CreateMap<CaseAction, GETCaseAction>();
            CreateMap<GETCaseAction, CaseAction>();
            CreateMap<CaseAction, PATCHCaseAction>();
            CreateMap<PATCHCaseAction, CaseAction>();

            // Report
            CreateMap<Report, POSTReport>();
            CreateMap<POSTReport, Report>();
            CreateMap<Report, GETReport>();
            CreateMap<GETReport, Report>();
        }
    }
}