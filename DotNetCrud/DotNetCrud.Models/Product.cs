using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCrud.Models
{
    public class Product
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public decimal Price { get; set; }

        public String Barcode { get; set; }

    }
}
