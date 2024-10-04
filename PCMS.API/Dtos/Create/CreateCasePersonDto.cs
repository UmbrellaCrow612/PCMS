using PCMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.Create
{
    /// <summary>
    /// DTO for POST data for linking a case and person.
    /// </summary>
    public class CreateCasePersonDto
    {
        [Required]
        [EnumDataType(typeof(CaseRole))]
        public required CaseRole Role { get; set; }
    }
}