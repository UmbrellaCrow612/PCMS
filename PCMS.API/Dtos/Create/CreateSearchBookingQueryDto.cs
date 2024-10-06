namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// Search params to search bookings by;
    /// </summary>
    public class CreateSearchBookingQueryDto
    {
        public string? Notes { get; set; } = null;

        public DateTime? BookingDate = null;

        public string? CreatedById { get; set; } = null;

        public string? PersonId { get; set; } = null;
    }
}
