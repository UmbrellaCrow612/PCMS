using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Dtos.PATCH
{
    /// <summary>
    /// DTO for when you want to update a department
    /// </summary
    public class UpdateDepartmentDto
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(250)]
        public required string Description { get; set; }

        [Required]
        [StringLength(20)]
        public required string ShortCode { get; set; }
    }
}