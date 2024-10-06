using Microsoft.EntityFrameworkCore;
using PCMS.API.BusinessLogic.Models.Audit;

namespace PCMS.API.Data
{
    public class AuditDbContext(DbContextOptions<AuditDbContext> options) : DbContext(options)
    {
        DbSet<TestAudit> TestAudits {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
