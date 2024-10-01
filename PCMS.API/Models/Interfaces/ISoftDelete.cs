namespace PCMS.API.Models.Interfaces
{
    /// <summary>
    /// Apply to models who can only be soft deleted and you want them to stay in the DB.
    /// </summary>
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAtUtc { get; set; }

        public string? DeletedById { get; set; }

        public ApplicationUser? UserWhoDeleted { get; set; }
    }
}
