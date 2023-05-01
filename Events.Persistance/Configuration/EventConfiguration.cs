using Events.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Persistance.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<EvenT>
    {
        public void Configure(EntityTypeBuilder<EvenT> builder)
        {
            builder.HasMany(x => x.Tickets).WithOne(x => x.Event);
        }
    }
}