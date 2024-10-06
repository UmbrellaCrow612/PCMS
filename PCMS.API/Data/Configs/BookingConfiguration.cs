using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedBookings).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.Person).WithMany(x => x.Bookings).HasForeignKey(x => x.PersonId);

            builder.HasMany(x => x.Charges).WithOne(x => x.Booking).HasForeignKey(x => x.BookingId);

            builder.HasOne(x => x.Release).WithOne(x => x.Booking).HasForeignKey<Booking>(x => x.ReleaseId);
        }
    }
}