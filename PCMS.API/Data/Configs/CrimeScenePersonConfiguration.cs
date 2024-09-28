using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CrimeScenePersonConfiguration : IEntityTypeConfiguration<CrimeScenePerson>
    {
        public void Configure(EntityTypeBuilder<CrimeScenePerson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CrimeScene).WithMany(x => x.CrimeScenePersons).HasForeignKey(x => x.CrimeSceneId);

            builder.HasOne(x => x.Person).WithMany(x => x.CrimeScenePersons).HasForeignKey(x => x.PersonId);
        }
    }
}