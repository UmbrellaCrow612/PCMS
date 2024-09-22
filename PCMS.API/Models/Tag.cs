using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    /// <summary>
    /// Represents a tag in the system used to tag elements and organise them.
    /// </summary>
    [Index(nameof(Id), IsUnique = true)]
    public class Tag
    {
        /// <summary>
        /// Gets or sets the Tag Id. Defaults to <see cref="Guid.NewGuid()"/>
        /// </summary>
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Gets or sets the name of the tag.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the tag.
        /// </summary>
        [MaxLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the date when the tag was created.
        /// Defaults to the current date and time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<CaseTag> CaseTags { get; set; } = [];
    }

}