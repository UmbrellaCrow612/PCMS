using PCMS.API.DTOS.GET;
using PCMS.API.Models;

namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO when you want to get a CaseNote
    /// </summary>
    public class CaseNoteDto
    {
        public required string Id { get; set; }

        public required string Description { get; set; }

        public required DateTime CreatedAtUtc { get; set; }

        public DateTime? LastModifiedAtUtc { get; set; }

        public required ApplicationUserDto Creator { get; set; }

        public required ApplicationUserDto LastModifiedBy { get; set; }

    }
}