using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class ApplicationUserCaseConfiguration : IEntityTypeConfiguration<ApplicationUserCase>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserCase> builder)
        {
            builder.HasKey(uc => new { uc.UserId, uc.CaseId });

            builder.HasOne(x => x.User).WithMany(x => x.AssignedCases).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Case).WithMany(x => x.AssignedUsers).HasForeignKey(x => x.CaseId);
        }
    }
}
