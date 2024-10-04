using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCMS.API.BusinessLogic.Models;

namespace PCMS.API.Data.Configs
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.CaseTags).WithOne(x => x.Tag).HasForeignKey(x => x.TagId);

            builder.HasOne(x => x.Creator).WithMany(x => x.CreatedTags).HasForeignKey(x => x.CreatedById);

            builder.HasOne(x => x.LastModifiedBy).WithMany(x => x.EditedTags).HasForeignKey(x => x.LastModifiedById);
        }
    }
}