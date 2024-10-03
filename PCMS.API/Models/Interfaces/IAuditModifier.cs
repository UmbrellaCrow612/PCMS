namespace PCMS.API.Models.Interfaces
{
    /// <summary>
    /// When you want to audit a model for it's editor.
    /// </summary>
    public interface IAuditModifier
    {
        DateTime? LastModifiedAtUtc { get; set; }

        string? LastModifiedById { get; set; }

        ApplicationUser? LastModifiedBy { get; set; }
    }
}
