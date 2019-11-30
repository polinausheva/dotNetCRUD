using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCrud.Web.Data.Models
{
    public class ProductGroup : IDataTableObject
    {
        [Key]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        [DisplayName("Category")]
        public string Name { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
