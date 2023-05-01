using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaProject.Domain;

namespace PizzaProject.Persistemce.Context
{
    public class PizzaProjectContext : DbContext
    {
        #region Ctor
        public PizzaProjectContext(DbContextOptions<PizzaProjectContext> options) :base(options) 
        {

        }
        #endregion
        //yoveli dbseti aris 1 cxrili dbshi
        #region DbSets 
        public DbSet<Pizza> Pizzas { get; set; }    
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        #endregion
        #region Configurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PizzaProjectContext).Assembly); 
            //ანუ მიიღე კონფიგურაციები ასემბლიდან რომელშიც მდებარეობს pizzaprojectcontext = ანუ persistance ასემბლიდან
        }

        #endregion
    }
}
