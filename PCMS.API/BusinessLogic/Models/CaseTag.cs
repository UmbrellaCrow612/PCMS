namespace PCMS.API.BusinessLogic.Models
{
    /// <summary>
    /// Many to many relation between Case and tag.
    /// </summary>
    public class CaseTag
    {
        public required string CaseId { get; set; }
        public Case? Case { get; set; } = null!;

        public required string TagId { get; set; }
        public Tag? Tag { get; set; } = null!;
    }
}