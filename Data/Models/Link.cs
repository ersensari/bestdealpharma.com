using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
  public class Link
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public int DisplayOrder { get; set; }
    public int Layout { get; set; }
    public int? ParentId { get; set; }
    [ForeignKey("Page")]
    public int? PageId { get; set; }

    public virtual Page Page { get; set; }

    [NotMapped]
    public List<Link> Children { get; set; }

  }
}
