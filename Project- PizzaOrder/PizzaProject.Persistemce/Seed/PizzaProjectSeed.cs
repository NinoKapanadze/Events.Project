using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.DependencyInjection;
using PizzaProject.Domain;
using PizzaProject.Persistemce.Context;

namespace PizzaProject.Persistemce.Seed
{
    public static class PizzaProjectSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<PizzaProjectContext>();
            Migrate(database);
            SeedEverything(database);
        }
        private static void Migrate(PizzaProjectContext context)
        {
            context.Database.Migrate(); //იგივეს აკეთებს რასაც აფდეით დატაბეისი
        }

        private static void SeedEverything(PizzaProjectContext context)
        {
            var seeded = false;
            SeedPizza(context, ref seeded);
            if (seeded)
            {
                context.SaveChanges();
            }
        }
        private static void SeedPizza(PizzaProjectContext context, ref bool seeded) //ref rashi gvinda movidzio ????????
        {
            var pizzas = new List<Pizza>
            {
                new Pizza
                {
                    Name = "Peperony",
                    Price = 45.99m,
                    Description = " pizza crust, pizza sauce, cheese, and pepperoni",
                    SecretIngredient = "cheese inside the crust",
                    CaloryCount = 494,
                    CreatedOn = DateTime.Now,
                    Id = ++Identifier
                },
                 new Pizza
                {
                    Name = "Margherita",
                    Price = 40.99m,
                    Description = " pizza crust, tomatoes, mozzarella cheese, garlic, fresh basil, and extra-virgin olive oil",
                    SecretIngredient = "pizza sauce",
                    CaloryCount = 550,
                    CreatedOn = DateTime.Now,
                    Id = ++Identifier
                }

            };

            foreach (var pizza in pizzas)
            {
                if( context.Pizzas.Any(x => x.Id == pizza.Id)) continue;

                context.Pizzas.Add(pizza);
                seeded = true;
               
            }

        }
    }
}
