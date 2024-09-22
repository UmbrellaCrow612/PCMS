using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.POST
{
    /// <summary>
    /// DTO for POST a department object
    /// </summary>
    public class POSTDepartment
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 100 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "ShortCode is required")]
        [StringLength(20, ErrorMessage = "ShortCode cannot be longer than 20 characters")]
        public required string ShortCode { get; set; }
    }
}