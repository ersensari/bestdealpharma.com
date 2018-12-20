using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace bestdealpharma.com.Data.Models
{
  public class Gallery
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageThumbUrl { get; set; }
    public string ImageLargeUrl { get; set; }
    [ForeignKey("Page")]
    public int PageId { get; set; }

    public virtual Page Page { get; set; }

    [NotMapped]
    public IFormFile File { get; set; }
  }
}
