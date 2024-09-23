﻿using PCMS.API.DTOS.GET;
using PCMS.API.Models;

namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO when GET a booking
    /// </summary>
    public class GETBooking
    {
        public required string Id { get; set; }

        public required DateTime BookingDate { get; set; }

        public required string Notes { get; set; }

        public required GETApplicationUser User { get; set; }

        public required GETPerson Person { get; set; }

        public Release? Release { get; set; }

        public required GETLocation Location { get; set; }
    }
}
