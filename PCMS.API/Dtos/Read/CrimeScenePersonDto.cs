﻿using PCMS.API.DTOS.Read;
using PCMS.API.Models.Enums;

namespace PCMS.API.Dtos.Read
{
    /// <summary>
    /// DTO when you get a crime scene person
    /// </summary>
    public class CrimeScenePersonDto
    {
        public required string Id { get; set; }

        public required CrimeSceneRole Role { get; set; }

        public required PersonDto Person { get; set; }
    }
}