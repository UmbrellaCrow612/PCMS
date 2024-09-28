using AutoMapper;
using PCMS.API.DTOS.GET;
using PCMS.API.DTOS.PATCH;
using PCMS.API.DTOS.POST;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<POSTPerson, Person>();
            CreateMap<Person, GETPerson>();
            CreateMap<PATCHPerson, Person>();
        }
    }
}