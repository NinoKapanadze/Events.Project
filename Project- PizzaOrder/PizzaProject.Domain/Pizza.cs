using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Domain
{
    public class Pizza: Base
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string SecretIngredient { get; set; }
       // public int Id { get; set; }
        public int CaloryCount { get; set; }
        //public bool IsDeleted { get; set; } = false;

        public static int Identifier = 0;

    }
}
