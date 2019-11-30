using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DotNetCrud.Web.Models;

namespace DotNetCrud.Web.Data.Models
{
    public class Purchase : IDataTableObject
    {
        [Key]
        public int Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual IList<Product> Products { get; set; }
        public DateTime DateFulfilled { get; set; }
    }
}
