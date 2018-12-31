using bestdealpharma.com.Data.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bestdealpharma.com.Data.Models
{

  public class Person
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string MobilePhone { get; set; }
    public string HomePhone { get; set; }
    public DateTime? BirthDate { get; set; }
    public string ZipCode { get; set; }
    public string State { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string UserId { get; set; }

  }
}
