using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS.POST
{
    /// <summary>
    /// DTO for POST data for linking a case and person.
    /// </summary>
    public class POSTCasePerson
    {
        [Required]
        [EnumDataType(typeof(CaseRole))]
        public required CaseRole Role { get; set; }
    }
}
