﻿using PCMS.API.Models;

namespace PCMS.API.DTOS.GET
{
    /// <summary>
    /// DTO when you want to get a Report
    /// </summary>
    public class GETReport
    {
        public required string Id { get; set; }

        public required string Title { get; set; }

        public required GETApplicationUser Creator { get; set; }

        public ApplicationUser? LastModifiedBy { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        public required string Details { get; set; }
    }
}