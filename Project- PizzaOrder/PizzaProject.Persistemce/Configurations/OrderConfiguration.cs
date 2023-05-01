using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaProject.Domain;

namespace PizzaProject.Persistemce.Configurations
{
    public  class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
    {

        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsUnicode(false).IsRequired();
    }
    
    }
}
