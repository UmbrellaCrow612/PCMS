using PCMS.API.DTOS.GET;
using PCMS.API.Models;

namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO to get a case edit
    /// </summary>
    public class GETCaseEdit
    {
        public required string Id { get; set; }

        public required GETApplicationUser User { get; set; }

        public required string PreviousTitle { get; set; }

        public required string PreviousDescription { get; set; }

        public required CaseStatus PreviousStatus { get; set; }

        public required CasePriority PreviousPriority { get; set; }

        public required string PreviousType { get; set; }

        public required DateTime CreatedAt { get; set; }
    }
}
