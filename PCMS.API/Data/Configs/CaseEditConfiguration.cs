using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CaseEditConfiguration : IEntityTypeConfiguration<CaseEdit>
    {
        public void Configure(EntityTypeBuilder<CaseEdit> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Creator).WithMany(x => x.CaseEdits).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.Case).WithMany(x => x.CaseEdits).HasForeignKey(x => x.CaseId);
        }
    }
}