using AutoMapper;
using PCMS.API.Dtos.GET;
using PCMS.API.Dtos.PATCH;
using PCMS.API.Dtos.POST;
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