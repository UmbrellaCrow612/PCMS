﻿namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// Represents the many-to-many relationship between ApplicationUser and Case.
    /// </summary>
    public class ApplicationUserCase
    {
        public required string UserId { get; set; }

        public ApplicationUser? User { get; set; } = null!;

        public required string CaseId { get; set; }

        public Case? Case { get; set; } = null!;
    }
}