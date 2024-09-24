using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class ChargeConfiguration : IEntityTypeConfiguration<Charge>
    {
        public void Configure(EntityTypeBuilder<Charge> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Booking).WithMany(x => x.Charges).HasForeignKey(x => x.BookingId);

            builder.HasOne(x => x.User).WithMany(x => x.Charges).HasForeignKey(x => x.UserId);
        }
    }
}
