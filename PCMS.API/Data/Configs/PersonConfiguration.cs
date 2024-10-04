using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IsDeleted).HasFilter("IsDeleted = 0");

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasMany(x => x.CasesInvolved).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);

            builder.HasMany(x => x.Bookings).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);

            builder.HasMany(x => x.Releases).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);

            builder.HasMany(x => x.Charges).WithOne(x => x.Person).HasForeignKey(x => x.PersonId);

            builder.HasOne(x => x.UserWhoDeleted).WithMany(x => x.DeletedPersons).HasForeignKey(x => x.DeletedById);
        }
    }
}