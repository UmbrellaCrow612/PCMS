﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(u => u.CreatedCases).WithOne(c => c.Creator).HasForeignKey(c => c.CreatedById);

            builder.HasMany(u => u.EditedCases).WithOne(c => c.LastEditor).HasForeignKey(c => c.LastEditedById).IsRequired(false);

            builder.HasMany(u => u.CreatedCaseActions).WithOne(ca => ca.Creator).HasForeignKey(ca => ca.CreatedById);

            builder.HasMany(u => u.EditedCaseActions).WithOne(ca => ca.LastEditor).HasForeignKey(ca => ca.LastEditedById).IsRequired(false);

            builder.HasMany(u => u.CreatedReports).WithOne(r => r.Creator).HasForeignKey(r => r.CreatedById);

            builder.HasMany(u => u.EditedReports).WithOne(r => r.LastEditor).HasForeignKey(r => r.LastEditedById).IsRequired(false);

            builder.HasMany(x => x.CreatedReleases).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.CreatedCharges).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.AssignedCases).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Department).WithMany(x => x.AssignedUsers).HasForeignKey(x => x.DepartmentId);

            builder.HasMany(x => x.CreatedBookings).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
