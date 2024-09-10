using PCMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    public class PATCHCase
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required")]
        public CaseStatus Status { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        public CasePriority Priority { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastModifiedById is required")]
        public string LastModifiedById { get; set; } = string.Empty;
    }
}
