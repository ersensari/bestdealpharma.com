using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string HtmlContent { get; set; }
    public string Manufacturer { get; set; }
    public string Strength { get; set; }
    public string Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsGeneric { get; set; }
    public bool OnlyRx { get; set; }
    public bool IsAvailable { get; set; }

  }
}
