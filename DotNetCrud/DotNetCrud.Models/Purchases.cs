using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCrud.Models
{
    public class Purchases
    {
        public int Id { get; set; }

        public IList<Product> Products { get; set; }

        public ApplicationUser MyProperty { get; set; }
    }
}
