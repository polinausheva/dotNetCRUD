using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace DotNetCrud.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual IList<Purchase> Purchases { get; set; }

    }
}
