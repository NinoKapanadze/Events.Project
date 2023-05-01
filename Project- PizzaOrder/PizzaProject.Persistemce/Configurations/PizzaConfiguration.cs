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
    public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
    {
        public void Configure(EntityTypeBuilder<Pizza> builder) 
        {
            //builder.ToTable("Pizza") am metods aqvs sashauleba tables pizzasgan gansxvavebuli saxeli davarqva, aseve calke nageti schirdeba
            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasKey(x => x.Id);// ar aris aucilebeli dawera, entity framework tavisit xvdeba rom id aris primary key
            builder.Property(x => x.Id).IsUnicode(false).IsRequired();
            //isunicode() დეფოლტად არის true ამიტომ char da varchars davuwerot IsUnicode(false)
        }
    }
}
