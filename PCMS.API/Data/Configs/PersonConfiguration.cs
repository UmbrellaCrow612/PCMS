using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.CasesInvolved).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);

            builder.HasMany(x => x.Bookings).WithOne(x => x.Person).HasForeignKey( x => x.PersonId);
        }
    }
}
