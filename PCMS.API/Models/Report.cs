using System.ComponentModel.DataAnnotations;

namespace PCMS.API.Models
{
    public class Report
    {

        public string Id = Guid.NewGuid().ToString();

        public required string CaseId { get; set; }

        public required Case Case { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}
