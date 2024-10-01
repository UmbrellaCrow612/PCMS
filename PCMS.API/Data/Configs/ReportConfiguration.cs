using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IsDeleted).HasFilter("IsDeleted = 0");

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedReports).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastEditor).WithMany(x => x.EditedReports).HasForeignKey(x => x.LastEditedById);

            builder.HasOne(x => x.Case).WithMany(x => x.Reports).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.UserWhoDeleted).WithMany(x => x.DeletedReports).HasForeignKey(x => x.DeletedById);

            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}