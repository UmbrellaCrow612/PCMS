using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CasePersonConfiguration : IEntityTypeConfiguration<CasePerson>
    {
        public void Configure(EntityTypeBuilder<CasePerson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Case).WithMany(x => x.PersonsInvolved).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.Person).WithMany(x => x.CasesInvolved).HasForeignKey(x => x.PersonId);
        }
    }
}
