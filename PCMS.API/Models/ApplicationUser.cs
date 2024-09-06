using Microsoft.AspNetCore.Identity;

namespace PCMS.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Rank { get; set; } = string.Empty;

        public string BadgeNumber { get; set; } = string.Empty;

        public DateTime DOB { get; set; } = DateTime.MinValue;


        public List<Case> AssignedCases { get; set; } = [];
    }
}
