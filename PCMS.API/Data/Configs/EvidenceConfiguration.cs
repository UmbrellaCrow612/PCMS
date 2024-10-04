using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class EvidenceConfiguration : IEntityTypeConfiguration<Evidence>
    {
        public void Configure(EntityTypeBuilder<Evidence> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IsDeleted).HasFilter("IsDeleted = 0");

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Case).WithMany(x => x.Evidences).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedEvidence).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastModifiedBy).WithMany(x => x.EditedEvidences).HasForeignKey(x => x.LastModifiedById);

            builder.HasOne(x => x.UserWhoDeleted).WithMany(x => x.DeletedEvidences).HasForeignKey(x => x.DeletedById);

        }
    }
}