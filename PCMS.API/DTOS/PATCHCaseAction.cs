﻿using System.ComponentModel.DataAnnotations;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO to PATCH a case action
    /// </summary>
    public class PATCHCaseAction
    {
        /// <summary>
        /// Gets or sets the case action name.
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action description.
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action type.
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the case action last edited by Id.
        /// </summary>
        [Required(ErrorMessage = "LastEditedById is required")]
        public string LastEditedById { get; set; } = string.Empty;
    }
}
