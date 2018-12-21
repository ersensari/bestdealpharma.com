using bestdealpharma.com.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Providers
{
  public interface IAuthenticatedPersonProvider
  {
    Person GetAuthenticatedPerson();
    Member GetAuthenticatedUser(string email);
  }
}
