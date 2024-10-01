using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class EvidenceConfiguration : IEntityTypeConfiguration<Evidence>
    {
        public void Configure(EntityTypeBuilder<Evidence> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IsDeleted).HasFilter("IsDeleted = 0");

            builder.HasOne(x => x.Case).WithMany(x => x.Evidences).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedEvidence).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastEditor).WithMany(x => x.EditedEvidences).HasForeignKey(x => x.LastEditedById);

            builder.HasOne(x => x.UserWhoDeleted).WithMany(x => x.DeletedEvidences).HasForeignKey(x => x.DeletedById);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}