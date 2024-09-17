using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a specific action taken on a case.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class CaseAction
    {
        /// <summary>
        /// Gets or sets the case action Id. Defaults to <see cref="Guid.NewGuid()"/>
        /// </summary>
        [Key]
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the case action name.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the case action description.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the case action type.
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public required string Type { get; set; }

        /// <summary>
        /// Gets or sets the case action created at time, defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        [Required(ErrorMessage = "CreatedAt is required")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the case action last modified at, defaults to <see cref="DateTime.UtcNow"/>.
        /// </summary>
        [Required(ErrorMessage = "LastModifiedDate is required")]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the case action created by Id.
        /// </summary>
        [Required(ErrorMessage = "CreatedById is required")]
        public required string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the case action last edited by Id.
        /// </summary>
        [Required(ErrorMessage = "LastEditedById is required")]
        public required string LastEditedById { get; set; }

        /// <summary>
        /// Gets or sets the specific case Id this case action is linked to, required.
        /// </summary>
        [Required(ErrorMessage = "CaseId is required")]
        public required string CaseId { get; set; }

        /// <summary>
        /// Gets or sets the specific case this case action is linked to, required.
        /// </summary>
        public Case? Case { get; set; } = null;
    }
}