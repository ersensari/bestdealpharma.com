using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace bestdealpharma.com.Data.Models
{
  public class OrderDetail
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Manufacturer { get; set; }
    public string Strength { get; set; }
    public string Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsGeneric { get; set; }
    public bool OnlyRx { get; set; }
    public int Amount { get; set; }
    public decimal TotalPrice { get; set; }

    [ForeignKey("Order")] public int OrderId { get; set; }
    public int Status { get; set; }
    public string StatusText { get; set; }

    public Order Order { get; set; }
  }
}
