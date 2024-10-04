using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IsDeleted).HasFilter("IsDeleted = 0");

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedCases).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastModifiedBy).WithMany(x => x.EditedCases).HasForeignKey(x => x.LastModifiedById);

            builder.HasOne(x => x.Department).WithMany(x => x.AssignedCases).HasForeignKey(x => x.DepartmentId);

            builder.HasMany(x => x.CaseActions).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.Reports).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.Evidences).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.PersonsInvolved).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.AssignedUsers).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.CaseNotes).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.CaseEdits).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.CaseTags).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.CrimeSceneCases).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasMany(x => x.CaseVehicles).WithOne(x => x.Case).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.UserWhoDeleted).WithMany(x => x.DeletedCases).HasForeignKey(x => x.DeletedById);
        }
    }
}