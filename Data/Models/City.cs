using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
