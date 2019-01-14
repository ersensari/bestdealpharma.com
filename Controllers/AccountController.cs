using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace bestdealpharma.com.Controllers
{
  [Route("[controller]/[action]")]
  public class AccountController : Controller
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    private readonly Providers.IAuthenticatedPersonProvider _authPerson;

    private readonly string[] AdminRoles = new string[] {"Admin", "Editor", "Call_Center"};

    public AccountController(
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager,
      RoleManager<IdentityRole> roleManager,
      IConfiguration configuration,
      Providers.IAuthenticatedPersonProvider authPerson
    )
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _authPerson = authPerson;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserModel login)
    {
      IActionResult response = Unauthorized();

      var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

      if (result.Succeeded)
      {
        IdentityUser user = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(user);
        if (login.forAdminPanel.GetValueOrDefault()
            && !roles.Any(x => AdminRoles.Contains(x)))
        {
          return Unauthorized();
        }

        var tokenString = GenerateJsonWebToken(user);
        response = Ok(new
        {
          token = tokenString, user = _authPerson.GetAuthenticatedUser(user.Email),
          returnUrl = login.forAdminPanel.GetValueOrDefault() ? "/admin" : "/"
        });
      }

      return response;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("index", "home");
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var identifier = new IdentityUser
      {
        UserName = model.Email,
        Email = model.Email,
        PasswordHash = Md5Hash(model.Password),
        PhoneNumber = model.MobilePhone,
        EmailConfirmed = false
      };
      var result = await _userManager.CreateAsync(identifier, model.Password);

      if (result.Succeeded)
      {
        await _signInManager.SignInAsync(identifier, false);
        IdentityUser user = await _userManager.GetUserAsync(User);
        await _userManager.AddToRoleAsync(user, "Guest");
        var tokenString = GenerateJsonWebToken(user);
        var response = Ok(new
        {
          token = tokenString, user = _authPerson.GetAuthenticatedUser(user.Email),
          returnUrl = "/account"
        });
        return response;
      }
      else
      {
        return BadRequest(result.Errors);
      }
    }

    private static string Md5Hash(string input)
    {
      using (var md5 = MD5.Create())
      {
        var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
        return Encoding.ASCII.GetString(result);
      }
    }

    private string GenerateJsonWebToken(IdentityUser user)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
      };
      var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
        _configuration["Jwt:Issuer"],
        claims,
        expires: DateTime.Now.AddMinutes(120),
        signingCredentials: credentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public class UserModel
    {
      [Required] public string Email { get; set; }

      [Required] public string Password { get; set; }

      public bool? forAdminPanel { get; set; }

      public bool isLockout { get; set; }
    }

    public class RegisterDto
    {
      [Required] public string Email { get; set; }

      [Required] public string Password { get; set; }

      [Required] public string Name { get; set; }
      [Required] public string Surname { get; set; }
      [Required] public string MobilePhone { get; set; }
      public string HomePhone { get; set; }
      [Required] public DateTime BirthDate { get; set; }
      [Required] public string Address { get; set; }
      [Required] public string City { get; set; }
      [Required] public string State { get; set; }
      [Required] public string ZipCode { get; set; }
      [Required] public string Country { get; set; }
    }
  }
}
