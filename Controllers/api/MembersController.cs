using bestdealpharma.com.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Controllers.api
{
  [ApiController]
  [Produces("application/json")]
  public class MembersController : ControllerBase
  {
    private readonly Data.DbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public MembersController(Data.DbContext context,
      UserManager<IdentityUser> userManager,
      RoleManager<IdentityRole> roleManager,
      SignInManager<IdentityUser> signInManager)
    {
      _context = context;
      _userManager = userManager;
      _roleManager = roleManager;
      _signInManager = signInManager;
    }

    // GET: api/members
    [Route("api/members")]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IEnumerable<Member> GetPeople()
    {

      if (!_roleManager.Roles.Any(x => x.Name == "Admin"))
        _roleManager.CreateAsync(new IdentityRole("Admin"));

      if (!_roleManager.Roles.Any(x => x.Name == "Editor"))
        _roleManager.CreateAsync(new IdentityRole("Editor"));

      if (!_roleManager.Roles.Any(x => x.Name == "Call_Center"))
        _roleManager.CreateAsync(new IdentityRole("Call_Center"));

      if (!_roleManager.Roles.Any(x => x.Name == "Guest"))
        _roleManager.CreateAsync(new IdentityRole("Guest"));

      var users = from p in _context.People
                  join u in _context.Users on p.UserId equals u.Id
                  join _roles in (from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id select new { role = r, ur.UserId }) on u.Id equals _roles.UserId into roles
                  select new Member { Person = p, User = u, Roles = roles.Select(x => x.role) };

      return users;
    }
    // GET: api/members/5
    [Route("api/members/{id}")]
    [HttpGet()]
    [Authorize]
    public async Task<IActionResult> GetPerson([FromRoute] int id)
    {
      if (id == -1)//isnew
      {
        return Ok(new Member { Person = new Person(), User = new IdentityUser(), Roles = new IdentityRole[] { } });
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var users = from p in _context.People
                  join u in _context.Users on p.UserId equals u.Id
                  join _roles in (from ur in _context.UserRoles join r in _context.Roles on ur.RoleId equals r.Id select new { role = r, ur.UserId }) on u.Id equals _roles.UserId into roles
                  select new Member { Person = p, User = u, Roles = roles.Select(x => x.role) };

      var person = await users.SingleOrDefaultAsync(m => m.Person.Id == id);

      if (person == null)
      {
        return NotFound();
      }

      return Ok(person);
    }

    // PUT: api/members/5
    [Route("api/members/{id}")]
    [HttpPut()]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Member member)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      using (var trn = _context.Database.BeginTransaction())
      {


        var person = member.Person;

        if (id != person.Id)
        {
          return BadRequest();
        }

        #region Update User
        var _user = await _userManager.FindByIdAsync(member.User.Id);
        _user.PhoneNumber = member.User.PhoneNumber;
        if (_user.PasswordHash != member.User.PasswordHash)
        {
          _user.PasswordHash = _userManager.PasswordHasher.HashPassword(_user, member.User.PasswordHash);
        }
        var identityResult = await _userManager.UpdateAsync(_user);
        if (!identityResult.Succeeded)
        {
          return BadRequest(identityResult.Errors.Select(x => new ErrorViewModel { Message = x.Description }));
        }
        #endregion

        #region Update Roles
        foreach (var role in _roleManager.Roles)
        {
          if (!await _userManager.IsInRoleAsync(_user, role.Name) && member.Roles.Any(x => x.Name == role.Name))
          {
            var identityRoleResult = await _userManager.AddToRoleAsync(_user, role.Name);
            if (!identityRoleResult.Succeeded)
            {
              return BadRequest(identityRoleResult.Errors.Select(x => new ErrorViewModel { Message = x.Description }));
            }
          }
          else if (await _userManager.IsInRoleAsync(_user, role.Name) && !member.Roles.Any(x => x.Name == role.Name))
          {
            var identityRoleResult = await _userManager.RemoveFromRoleAsync(_user, role.Name);
            if (!identityRoleResult.Succeeded)
            {
              return BadRequest(identityRoleResult.Errors.Select(x => new ErrorViewModel { Message = x.Description }));
            }
          }
        }


        #endregion

        #region lockout
        await _userManager.SetLockoutEnabledAsync(_user, member.User.LockoutEnabled);
        #endregion


        _context.Entry(person).State = EntityState.Modified;

        try
        {
          await _context.SaveChangesAsync();
          trn.Commit();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!PersonExists(id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
      }
      return Ok(member);
    }

    // POST: api/members
    [Route("api/members")]
    [HttpPost]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> PostPerson([FromBody] Member member)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      using (var trn = _context.Database.BeginTransaction())
      {
        _context.People.Add(member.Person);
        member.Person.UserId = member.User.Id;

        #region Update User
        var _user = await _userManager.FindByEmailAsync(member.User.Email);
        if (_user != null)
        {
          return BadRequest(new ErrorViewModel[] { new ErrorViewModel() { Message = "Aynı eposta ile daha önce kullanıcı oluşturulmuş" } });
        }

        member.User.SecurityStamp = Guid.NewGuid().ToString();
        var identityResult = await _userManager.CreateAsync(member.User, member.User.PasswordHash);
        if (!identityResult.Succeeded)
        {
          return BadRequest(identityResult.Errors.Select(x => new ErrorViewModel { Message = x.Description }));
        }
        #endregion

        #region Update Roles
        foreach (var role in _roleManager.Roles)
        {
          if (!await _userManager.IsInRoleAsync(member.User, role.Name) && member.Roles.Any(x => x.Name == role.Name))
          {
            var identityRoleResult = await _userManager.AddToRoleAsync(member.User, role.Name);
            if (!identityRoleResult.Succeeded)
            {
              return BadRequest(identityRoleResult.Errors.Select(x => new ErrorViewModel { Message = x.Description }));
            }
          }
          else if (await _userManager.IsInRoleAsync(member.User, role.Name) && !member.Roles.Any(x => x.Name == role.Name))
          {
            var identityRoleResult = await _userManager.RemoveFromRoleAsync(member.User, role.Name);
            if (!identityRoleResult.Succeeded)
            {
              return BadRequest(identityRoleResult.Errors.Select(x => new ErrorViewModel { Message = x.Description }));
            }
          }
        }


        #endregion

        await _context.SaveChangesAsync();
        trn.Commit();
      }
      return CreatedAtAction("GetPerson", new { id = member.Person.Id }, member);
    }

    // DELETE: api/members/5
    [Route("api/members/{id}")]
    [Authorize(Roles = "Admin")]

    [HttpDelete()]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
      if (person == null)
      {
        return NotFound();
      }

      _context.People.Remove(person);
      await _context.SaveChangesAsync();

      return Ok(person);
    }

    private bool PersonExists(int id)
    {
      return _context.People.Any(e => e.Id == id);
    }
  }
}
