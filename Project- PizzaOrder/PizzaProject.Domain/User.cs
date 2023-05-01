using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaProject.Domain
{
    public class User
    {
        //public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public List<Address> UserAddress { get; set; }
        //public bool IsDeleted { get; set; } = false;

        public static int Identifier = 1;
    }
}
