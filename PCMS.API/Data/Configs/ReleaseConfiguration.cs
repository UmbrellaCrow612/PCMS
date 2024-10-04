using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class ReleaseConfiguration : IEntityTypeConfiguration<Release>
    {
        public void Configure(EntityTypeBuilder<Release> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User).WithMany(x => x.CreatedReleases).HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Booking).WithOne(x => x.Release).HasForeignKey<Release>(x => x.BookingId);

            builder.HasOne(x => x.Person).WithMany(x => x.Releases).HasForeignKey(x => x.PersonId);
        }
    }
}