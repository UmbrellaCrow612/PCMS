namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO to GET a Tag
    /// </summary>
    public class GETTag
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
