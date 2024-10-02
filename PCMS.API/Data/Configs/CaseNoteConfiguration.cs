using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CaseNoteConfiguration : IEntityTypeConfiguration<CaseNote>
    {
        public void Configure(EntityTypeBuilder<CaseNote> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IsDeleted).HasFilter("IsDeleted = 0");

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Case).WithMany(x => x.CaseNotes).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedCaseNotes).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastModifiedBy).WithMany(x => x.EditedCaseNotes).HasForeignKey(x => x.LastModifiedById);

            builder.HasOne(x => x.UserWhoDeleted).WithMany(x => x.DeletedCaseNotes).HasForeignKey(x => x.DeletedById);
        }
    }
}