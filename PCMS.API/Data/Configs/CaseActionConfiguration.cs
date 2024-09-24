using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CaseActionConfiguration : IEntityTypeConfiguration<CaseAction>
    {
        public void Configure(EntityTypeBuilder<CaseAction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedCaseActions).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastEditor).WithMany(x => x.EditedCaseActions).HasForeignKey(x => x.LastEditedById);

            builder.HasOne(x => x.Case).WithMany(x => x.CaseActions).HasForeignKey(x => x.CaseId);
        }
    }
}
