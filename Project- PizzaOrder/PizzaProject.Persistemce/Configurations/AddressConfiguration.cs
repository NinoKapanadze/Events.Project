using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PizzaProject.Domain;

namespace PizzaProject.Persistemce.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsUnicode(false).IsRequired();
        }
    }
}
