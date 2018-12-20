using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Data.Models
{
  public class Currency
  {
    public int Id { get; set; }
    public bool m_success { get; set; }
    public long m_timestamp { get; set; }
    public string m_base { get; set; }
    public DateTime m_date { get; set; }

    public IList<CurrencyRate> CurrencyRates { get; set; }

  }
}
