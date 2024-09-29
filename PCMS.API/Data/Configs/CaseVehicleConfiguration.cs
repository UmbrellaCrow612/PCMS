using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CaseVehicleConfiguration : IEntityTypeConfiguration<CaseVehicle>
    {
        public void Configure(EntityTypeBuilder<CaseVehicle> builder)
        {
            builder.HasKey(x => new {x.CaseId, x.VehicleId });

            builder.HasOne(x => x.Case).WithMany(x => x.CaseVehicles).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.Vehicle).WithMany(x => x.CaseVehicles).HasForeignKey(x => x.VehicleId);
        }
    }
}
