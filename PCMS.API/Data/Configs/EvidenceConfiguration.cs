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

            builder.HasOne(x => x.Case).WithMany(x => x.Evidences).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedEvidence).HasForeignKey(x => x.CreatedById);
        }
    }
}
