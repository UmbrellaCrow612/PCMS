using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<CreatePersonDto, Person>();
            CreateMap<Person, PersonDto>();
            CreateMap<UpdatePersonDto, Person>();
        }
    }
}