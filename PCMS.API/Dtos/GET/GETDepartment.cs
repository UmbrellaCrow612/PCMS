namespace PCMS.API.Dtos.GET
{
    /// <summary>
    /// DTO when you want to get a Department
    /// </summary>
    public class GETDepartment
    {
        public required string Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string ShortCode { get; set; }
    }
}