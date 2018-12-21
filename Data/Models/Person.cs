using bestdealpharma.com.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace bestdealpharma.com.Data.Models
{

  public class Person
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public string UserId { get; set; }

  }
}
