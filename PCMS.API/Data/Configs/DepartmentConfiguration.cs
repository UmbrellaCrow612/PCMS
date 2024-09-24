using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.AssignedUsers).WithOne(x => x.Department).HasForeignKey(x => x.DepartmentId);

            builder.HasMany(x => x.AssignedCases).WithOne(x => x.Department).HasForeignKey(x => x.DepartmentId);
;        }
    }
}
