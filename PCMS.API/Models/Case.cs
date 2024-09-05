namespace PCMS.API.Models
{
    public class Case
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public CaseStatus Status { get; set; } = CaseStatus.Open;

        public DateTime DateOpened { get; set; } = DateTime.Now;

        public DateTime? DateClosed { get; set; }

        public CasePriority Priority { get; set; }

        public string Type { get; set; } = string.Empty;

        public List<ApplicationUser> AssignedUsers { get; set; } = [];

    }

    public enum CaseStatus
    {
        Open = 0,
        Closed,
        InProgress,
        OnHold,
        Resolved
    }

    public enum CasePriority
    {
        Low = 0,
        Medium,
        High,
        Critical
    }


}
