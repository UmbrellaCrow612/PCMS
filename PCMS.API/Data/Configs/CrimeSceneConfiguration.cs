using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CrimeSceneConfiguration : IEntityTypeConfiguration<CrimeScene>
    {
        public void Configure(EntityTypeBuilder<CrimeScene> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Location).WithMany(x => x.CrimeScenes).HasForeignKey(x => x.LocationId);

            builder.HasMany(x => x.CrimeSceneCases).WithOne(x => x.CrimeScene).HasForeignKey(x => x.CrimeSceneId);

            builder.HasMany(x => x.CrimeScenePersons).WithOne(x => x.CrimeScene).HasForeignKey(x => x.CrimeSceneId);
        }
    }
}
