using DotNetCrud.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCrud.Data
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
