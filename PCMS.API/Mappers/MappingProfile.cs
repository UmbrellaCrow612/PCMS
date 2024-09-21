using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
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
            CreateMap<Report, PATCHReport>();
            CreateMap<PATCHReport, Report>();

            // Evidence
            CreateMap<Evidence, POSTEvidence>();
            CreateMap<POSTEvidence, Evidence>();
            CreateMap<Evidence, GETEvidence>();
            CreateMap<GETEvidence, Evidence>();
            CreateMap<Evidence, PATCHEvidence>();
            CreateMap<PATCHEvidence, Evidence>();

            // Persons
            CreateMap<Person, POSTPerson>();
            CreateMap<POSTPerson, Person>();
            CreateMap<Person, GETPerson>();
            CreateMap<GETPerson, Person>();
            CreateMap<Person, PATCHPerson>();
            CreateMap<PATCHPerson, Person>();

            // case person
            CreateMap<CasePerson, POSTCasePerson>();
            CreateMap<POSTCasePerson, CasePerson>();
            CreateMap<CasePerson, GETCasePerson>();
            CreateMap<GETCasePerson, CasePerson>();

            // Application user case
            CreateMap<ApplicationUserCase, GETApplicationUserCase>();
            CreateMap<GETApplicationUserCase, ApplicationUserCase>();

            // Locations
            CreateMap<Location, POSTLocation>();
            CreateMap<POSTLocation, Location>();
            CreateMap<Location, GETLocation>();
            CreateMap<GETLocation, Location>();
            CreateMap<Location, PATCHLocation>();
            CreateMap<PATCHLocation, Location>();

            // Property
            CreateMap<Property, POSTProperty>();
            CreateMap<POSTProperty, Property>();
            CreateMap<Property, GETProperty>();
            CreateMap<GETProperty, Property>();
            CreateMap<Property, PATCHProperty>();
            CreateMap<PATCHProperty, Property>();

            // Departments
            CreateMap<Department, POSTDepartment>();
            CreateMap<POSTDepartment, Department>();
            CreateMap<Department, GETDepartment>();
            CreateMap<GETDepartment, Department>();
            CreateMap<Department, PATCHDepartment>();
            CreateMap<PATCHDepartment, Department>();

            // Case Note
            CreateMap<CaseNote, POSTCaseNote>();
            CreateMap<POSTCaseNote, CaseNote>();
            CreateMap<CaseNote, GETCaseNote>();
            CreateMap<GETCaseNote, CaseNote>();
            CreateMap<CaseNote, PATCHCaseNote>();
            CreateMap<PATCHCaseNote, CaseNote>();

            // Case edit
            CreateMap<Case, CaseEdit>()
                        .ForMember(dest => dest.PreviousTitle, opt => opt.MapFrom(src => src.Title))
                        .ForMember(dest => dest.PreviousDescription, opt => opt.MapFrom(src => src.Description))
                        .ForMember(dest => dest.PreviousStatus, opt => opt.MapFrom(src => src.Status))
                        .ForMember(dest => dest.PreviousPriority, opt => opt.MapFrom(src => src.Priority))
                        .ForMember(dest => dest.PreviousType, opt => opt.MapFrom(src => src.Type));

            CreateMap<CaseEdit, GETCaseEdit>();
        }
    }
}