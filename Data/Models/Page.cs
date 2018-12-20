using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
  public class Page
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string HtmlContent { get; set; }
    public string MetaDescription { get; set; }
    public int? Module { get; set; }

    public virtual IEnumerable<Gallery> Galleries { get; set; }
  }
}
