namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO when you want to get a Tag
    /// </summary>
    public class GETTag
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}