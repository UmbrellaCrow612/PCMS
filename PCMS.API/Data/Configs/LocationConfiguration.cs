using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Properties).WithOne(x => x.Location).HasForeignKey(x => x.LocationId);

            builder.HasMany(x => x.Bookings).WithOne(x => x.Location).HasForeignKey(x => x.LocationId);
        }
    }
}