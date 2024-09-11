using PCMS.API.Models;

namespace PCMS.API.DTOS
{
    /// <summary>
    /// DTO for GET a case object
    /// </summary>
    public class GETCase
    {
        /// <summary>
        /// Gets or sets the Case Id.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case Number.
        /// </summary>
        public string CaseNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Case status based on the <see cref="CaseStatus"/> enum.
        /// </summary>
        public CaseStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the time the case was opened defaults to now.
        /// </summary>
        public DateTime DateOpened { get; set; }

        /// <summary>
        /// Gets or sets the time the case was closed defaults to null.
        /// </summary>
        public DateTime? DateClosed { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the case was last modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the case priority based on the <see cref="CasePriority"/> enum.
        /// </summary>
        public CasePriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the case type.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user Id Who created the case.
        /// </summary>
        public required string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the user Id who last modified the case.
        /// </summary>
        public required string LastModifiedById { get; set; }

        /// <summary>
        /// Gets or sets the case actions, list of CaseAction.
        /// </summary>
        public List<GETCaseAction> CaseActions { get; set; } = [];

        /// <summary>
        /// Gets or sets the case assigned users, list of ApplicationUser.
        /// </summary>
        public List<GETApplicationUser> AssignedUsers { get; set; } = [];

        /// <summary>
        /// Gets or sets the case reports, list of Report.
        /// </summary>
        public List<Report> Reports { get; set; } = [];


    }
}
