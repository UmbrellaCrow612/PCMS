﻿using PCMS.API.Filters;
using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for POST data for linking a case and person.
    /// </summary>
    public class POSTLinkCasePerson
    {
        [Required]
        [ValidEnumValue(typeof(CaseRole))]
        public required CaseRole Role { get; set; }
    }
}
