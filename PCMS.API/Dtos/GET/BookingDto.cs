using PCMS.API.DTOS.GET;
using PCMS.API.Models;

namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO when you want to get a <see cref="Booking"/>
    /// </summary>
    public class BookingDto
    {
        public required string Id { get; set; }

        public required DateTime BookingDate { get; set; }

        public required string Notes { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        public ApplicationUser? Creator { get; set; }

        public ApplicationUser? LastModifiedBy { get; set; }

        public required PersonDto Person { get; set; }

        public ReleaseDto? Release { get; set; }

        public ICollection<ChargeDto> Charges { get; set; } = [];

        public required LocationDto Location { get; set; }
    }
}