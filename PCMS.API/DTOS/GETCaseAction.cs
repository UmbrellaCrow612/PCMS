﻿using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for GET a case action
    /// </summary>
    public class GETCaseAction
    {
        /// <summary>
        /// Gets or sets the case action Id.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action type.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action created at time, defaults to now.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the case action last modified at.
        /// </summary>
        public DateTime LastModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the case action created by Id.
        /// </summary>
        public string CreatedById { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action last edited by Id.
        /// </summary>
        public string LastEditedById { get; set; } = string.Empty;

    }
}
