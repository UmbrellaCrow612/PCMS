using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(u => u.CreatedCases).WithOne(c => c.Creator).HasForeignKey(c => c.CreatedById);

            builder.HasMany(u => u.EditedCases).WithOne(c => c.LastModifiedBy).HasForeignKey(c => c.LastModifiedById).IsRequired(false);

            builder.HasMany(u => u.CreatedCaseActions).WithOne(ca => ca.Creator).HasForeignKey(ca => ca.CreatedById);

            builder.HasMany(u => u.EditedCaseActions).WithOne(ca => ca.LastModifiedBy).HasForeignKey(ca => ca.LastModifiedById).IsRequired(false);

            builder.HasMany(u => u.CreatedReports).WithOne(r => r.Creator).HasForeignKey(r => r.CreatedById);

            builder.HasMany(u => u.EditedReports).WithOne(r => r.LastModifiedBy).HasForeignKey(r => r.LastModifiedById).IsRequired(false);

            builder.HasMany(x => x.CreatedReleases).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.CreatedCharges).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.AssignedCases).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Department).WithMany(x => x.AssignedUsers).HasForeignKey(x => x.DepartmentId);

            builder.HasMany(x => x.CreatedBookings).WithOne(x => x.Creator).HasForeignKey(x => x.CreatedById);
        }
    }
}