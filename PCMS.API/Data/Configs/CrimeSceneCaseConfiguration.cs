using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.Models;

namespace PCMS.API.Data.Configs
{
    public class CrimeSceneCaseConfiguration : IEntityTypeConfiguration<CrimeSceneCase>
    {
        public void Configure(EntityTypeBuilder<CrimeSceneCase> builder)
        {
            builder.HasKey(x => new { x.CaseId, x.CrimeSceneId });

            builder.HasOne(x => x.Case).WithMany(x => x.CrimeSceneCases).HasForeignKey(x => x.CaseId);

            builder.HasOne(x => x.CrimeScene).WithMany(x => x.CrimeSceneCases).HasForeignKey(x => x.CrimeSceneId);
        }
    }
}
