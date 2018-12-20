using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
  public class Town
  {
    public int Id { get; set; }
    [ForeignKey("City")]
    public int CityId { get; set; }
    public string Name { get; set; }

    public virtual City City { get; set; }
  }
}
