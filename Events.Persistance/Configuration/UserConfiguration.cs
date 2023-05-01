using Event.Web.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Persistance.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.HasMany(x => x.Events).WithOne(x => x.User).HasForeignKey(x => x.BaseUserId);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Role).IsRequired().HasMaxLength(50).HasConversion<string>();
        }
    }
}