using bestdealpharma.com.Data;
using bestdealpharma.com.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bestdealpharma.com.Providers
{
  public class AuthenticatedPersonProvider : IAuthenticatedPersonProvider
  {
    private readonly DbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedPersonProvider(DbContext context, IHttpContextAccessor httpContextAccessor)
    {
      _context = context;
      _httpContextAccessor = httpContextAccessor;
    }
    public Person GetAuthenticatedPerson()
    {
      var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
      var person = _context.People.FirstOrDefault(x => x.UserId == userId);
      return person;
    }

    public Member GetAuthenticatedUser(string email)
    {
      string userId = string.Empty;
      if (string.IsNullOrWhiteSpace(email))
      {
        userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
      }

      var users = from p in _context.People
                  join u in _context.Users on p.UserId equals u.Id
                  join _roles in (from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id select new { role = r, ur.UserId }) on u.Id equals _roles.UserId into roles
                  select new Member { Person = p, User = u, Roles = roles.Select(x => x.role) };

      var member = users.SingleOrDefault(m => (userId == string.Empty && m.User.Email == email) || m.User.Id == userId);
      return member;
    }
  }
}
