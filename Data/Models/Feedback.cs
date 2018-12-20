using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
  public class Feedback
  {
    public int Id { get; set; }
    [StringLength(500)]
    public string Title { get; set; }
    [Required(AllowEmptyStrings = false)]
    [StringLength(50)]
    public string Name { get; set; }
    [Required(AllowEmptyStrings = false)]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    [StringLength(250)]
    public string Email { get; set; }
    public string Phone { get; set; }
    [Required(AllowEmptyStrings = false)]
    [DataType(DataType.Text)]
    [StringLength(2000)]
    public string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public string IpAddress { get; set; }
    public string PhotoUrl { get; set; }
    public bool Approved { get; set; }

    [NotMapped]
    public IFormFile PhotoFile { get; set; }

  }
}
