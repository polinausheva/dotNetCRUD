using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCrud.Web.Data.Models
{
    public class Faq : IDataTableObject
    {
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Question { get; set; }

        public string Answer { get; set; }
    }
}