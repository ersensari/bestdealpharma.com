using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
  public class Member
  {
    public Person Person { get; set; }
    public ApplicationUser User { get; set; }
    public IEnumerable<IdentityRole> Roles { get; set; }
  }
}
