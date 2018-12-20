using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
    public class CurrencyRate
    {
        public int Id { get; set; }
        public string name { get; set; }
        public decimal amount { get; set; }
        public int currencyId { get; set; }

    }
}
