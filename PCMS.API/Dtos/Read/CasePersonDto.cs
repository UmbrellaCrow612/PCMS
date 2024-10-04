using PCMS.API.Models;
using PCMS.API.Models.Enums;

namespace PCMS.API.DTOS.Read
{
    /// <summary>
    /// DTO when you want to get a <see cref="CasePerson"/>
    /// </summary>
    public class CasePersonDto
    {
        public required string Id { get; set; }
        public required PersonDto Person { get; set; }
        public required CaseRole Role { get; set; }
    }
}