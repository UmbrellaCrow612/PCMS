﻿using AutoMapper;
using PCMS.API.BusinessLogic.Models;
using PCMS.API.Dtos.Create;
using PCMS.API.DTOS.Read;
using PCMS.API.DTOS.Update;

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