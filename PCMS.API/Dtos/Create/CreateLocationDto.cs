﻿using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.Create
{
    /// <summary>
    /// DTO to POST a location.
    /// </summary>
    public class CreateLocationDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        [StringLength(200)]
        public required string Address { get; set; }

        [Required]
        [StringLength(50)]
        public required string City { get; set; }

        [Required]
        [StringLength(20)]
        public required string PostalCode { get; set; }

        [Required]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public required decimal Latitude { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public required decimal Longitude { get; set; }
    }
}