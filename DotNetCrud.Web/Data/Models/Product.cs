using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCrud.Web.Data.Models
{
    public class Product : IDataTableObject
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public String Name { get; set; }

        public String Description { get; set; }

        public decimal Price { get; set; }

        public String Barcode { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
        public int ProductGroupId { get; set; }

        public virtual IList<Purchase> Purchases { get; set; }
    }
}
