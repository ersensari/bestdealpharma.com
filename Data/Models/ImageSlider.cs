using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
  public class ImageSlider
  {
    public int Id { get; set; }
    public string Header1 { get; set; }
    public string Header2 { get; set; }
    public string ImageUrl { get; set; }
    public string DetailUrl { get; set; }
    public int DisplayOrder { get; set; }

    [NotMapped]
    public IFormFile ImageFile { get; set; }
  }
}
