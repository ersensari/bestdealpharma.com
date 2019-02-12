
using System.ComponentModel.DataAnnotations.Schema;

namespace bestdealpharma.com.Data.Models
{
  public class Address
  {
    public int Id { get; set; }
    public string AddressName { get; set; }
    public string MobilePhone { get; set; }
    public string ZipCode { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string AddressLine { get; set; }
    [ForeignKey("Person")]
    public int PersonId { get; set; }

    public virtual Person Person { get; set; }
  }
}
