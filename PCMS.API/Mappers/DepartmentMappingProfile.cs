using AutoMapper;
using PCMS.API.Dtos.Create;
using PCMS.API.Dtos.Read;
using PCMS.API.Dtos.Update;
using PCMS.API.Models;

namespace PCMS.API.Mappers
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<UpdateDepartmentDto, Department>();
        }
    }
}