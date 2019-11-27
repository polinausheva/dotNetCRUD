using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCrud.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual IList<Product> Products { get; set; }
        public DateTime DateFulfilled { get; set; }
    }
}
