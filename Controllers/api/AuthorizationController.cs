using bestdealpharma.com.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bestdealpharma.com.Controllers.api
{
  [ApiController]
  [Authorize]
  public class AuthorizationController : ControllerBase
  {
    private readonly Providers.IAuthenticatedPersonProvider _authPerson;
    public AuthorizationController(Providers.IAuthenticatedPersonProvider authPerson)
    {
      _authPerson = authPerson;
    }

    [Route("api/Authorization/GetAuthenticatedPerson")]
    public Person GetAuthenticatedPerson()
    {
      return _authPerson.GetAuthenticatedPerson();
    }

    [Route("api/Authorization/GetAuthenticatedUser")]
    public Member GetAuthenticatedUser()
    {
      return _authPerson.GetAuthenticatedUser(string.Empty);
    }
  }
}
