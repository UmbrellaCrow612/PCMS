namespace PCMS.API.Dtos.Read
{
    /// <summary>
    /// DTO when you want to get a charge
    /// </summary>
    public class ChargeDto
    {
        public required string Id { get; set; }

        public required string Offense { get; set; }

        public required DateTime DateCharged { get; set; }

        public required string Description { get; set; }
    }
}