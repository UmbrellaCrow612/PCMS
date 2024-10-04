using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.DTOS.Read
{
    /// <summary>
    /// DTO when you want to get a Report
    /// </summary>
    public class ReportDto
    {
        public required string Id { get; set; }

        public required string Title { get; set; }

        public required ApplicationUserDto Creator { get; set; }

        public ApplicationUser? LastModifiedBy { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        public required string Details { get; set; }
    }
}