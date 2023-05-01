using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }

        public int PizzaId { get; set; }
        //public List<Pizza> OrderPizzaList { get; set; }
       // public bool IsDeleted { get; set; } = false;

        public static int Identifier = 1;


    }
}
