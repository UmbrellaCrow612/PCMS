using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// Represents a Department in the system.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Department
    {
        /// <summary>
        /// Gets or sets the ID of the Department, defaults to <see cref="Guid.NewGuid()"/>.
        /// </summary>
        [Key]
        public string Id = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the Name of the Department.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 100 characters")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description of the Department.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters")]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the ShortCode of the Department.
        /// </summary>
        [Required(ErrorMessage = "ShortCode is required")]
        [StringLength(20, ErrorMessage = "ShortCode cannot be longer than 20 characters")]
        public required string ShortCode { get; set; }

        /// <summary>
        /// Ef core.
        /// </summary>
        public ICollection<ApplicationUser> AssignedUsers { get; } = [];

        /// <summary>
        /// Ef core.
        /// </summary>
        public ICollection<Case> AssignedCases { get; } = [];
    }
}