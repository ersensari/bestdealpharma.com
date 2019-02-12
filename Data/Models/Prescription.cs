using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace bestdealpharma.com.Data.Models
{
  public class Prescription
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime UploadDate { get; set; }
    public string FileName { get; set; }

    [ForeignKey("Person")]
    public int PersonId { get; set; }

    public Person Person { get; set; }
  }
}
