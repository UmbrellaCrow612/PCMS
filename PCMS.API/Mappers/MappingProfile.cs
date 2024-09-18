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
        }
    }
}