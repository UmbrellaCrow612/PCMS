using PCMS.API.BusinessLogic.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// List of paramas sent across when searching for cases, which will be used to filter for cases based on the params.
    /// </summary>
    /// <remarks>
    /// When sending these search paramas keep fields that have no value as null or you are not trying to search with becuase if its empty string it will try to match those.
    /// </remarks>
    public class CreateSearchCasesQueryDto
    {
        public string? Title { get; set; } = null;

        public string? Description { get; set; } = null;

        [EnumDataType(typeof(CaseStatus))]
        public CaseStatus? Status { get; set; } = null;

        public DateTime? DateOpened { get; set; } = null;

        public DateTime? DateClosed { get; set; } = null;

        [EnumDataType(typeof(CasePriority))]
        public CasePriority? Priority { get; set; } = null;

        public string? Type { get; set; } = null;

        [EnumDataType(typeof(CaseComplexity))]
        public CaseComplexity? Complexity { get; set; } = null;

        public string? CreatedById { get; set; } = null;

        public string? DepartmentId { get; set; } = null;
    }
}
