﻿using PCMS.API.Models;
using PCMS.API.Models.Enums;

namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO when you want to get a <see cref="CasePerson"/>
    /// </summary>
    public class GETCasePerson
    {
        public required string Id { get; set; }
        public required GETPerson Person { get; set; }
        public required CaseRole Role { get; set; }
    }
}