using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class CaseTagConfiguration : IEntityTypeConfiguration<CaseTag>
    {
        public void Configure(EntityTypeBuilder<CaseTag> builder)
        {
            builder.HasKey(x => new { x.TagId, x.CaseId });

            builder.HasOne(x => x.Case).WithMany(x => x.CaseTags).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.Tag).WithMany(x => x.CaseTags).HasForeignKey(x => x.TagId);
        }
    }
}