using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedCases).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastEditor).WithMany(x => x.EditedCases).HasForeignKey(x => x.LastEditedById);

            builder.HasOne(x => x.Department).WithMany(x => x.AssignedCases).HasForeignKey(x => x.DepartmentId);

            builder.HasMany(x => x.CaseActions).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.Reports).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.Evidences).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.PersonsInvolved).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.AssignedUsers).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.CaseNotes).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.CaseEdits).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.CaseTags).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);
        }
    }
}