using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Remotion.Linq.Clauses.ResultOperators;

namespace bestdealpharma.com.Data.Models
{
  public class Order
  {
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string MobilePhone { get; set; }
    public string ZipCode { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string AddressLine { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Shipping { get; set; }
    public string ShippingMethod { get; set; }
    public decimal Total { get; set; }
    public string CustomerExplanation { get; set; }
    public string ServiceExplanation { get; set; }
    public string ShippingLink { get; set; }
    public bool? Archived { get; set; }
    public int Status { get; set; }
    [ForeignKey("Person")] public int PersonId { get; set; }
    [ForeignKey("Prescription")] public int PrescriptionId { get; set; }
    public Person Person { get; set; }
    public Prescription Prescription { get; set; }

    public IEnumerable<OrderDetail> OrderDetails { get; set; }
  }
}
