using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using bestdealpharma.com.Data.Models;
using bestdealpharma.com.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace bestdealpharma.com.Controllers
{
  [Route("[controller]/[action]")]
  public class AccountController : Controller
  {
    private readonly Data.DbContext _context;


    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    private readonly Providers.IAuthenticatedPersonProvider _authPerson;

    private readonly string[] _adminRoles = new string[] {"Admin", "Editor", "Call_Center"};

    private readonly IEmailService _emailService;


    public AccountController(
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager,
      RoleManager<IdentityRole> roleManager,
      IConfiguration configuration,
      Providers.IAuthenticatedPersonProvider authPerson,
      Data.DbContext context,
      IEmailService emailService
    )
    {
      _context = context;
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _authPerson = authPerson;
      _emailService = emailService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserModel login)
    {
      IActionResult response = Unauthorized();

      var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

      if (!result.Succeeded) return response;

      var identifier = new IdentityUser
      {
        UserName = login.Email,
        Email = login.Email
      };


      var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(identifier.Email));
      if (login.forAdminPanel.GetValueOrDefault()
          && !roles.Any(x => _adminRoles.Contains(x)))
      {
        return Unauthorized();
      }

      var tokenString = GenerateJsonWebToken(identifier);
      response = Ok(new
      {
        token = tokenString,
        user = _authPerson.GetAuthenticatedUser(identifier.Email),
        returnUrl = login.forAdminPanel.GetValueOrDefault() ? "/admin" : "/"
      });

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
    [AllowAnonymous]
    public async Task<IActionResult> SendRescueCode([FromBody] UserModel model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user != null)
      {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        await _emailService.SendEmail(user.Email, "bestdealpharma.com Password Recovery",
          $"<a href='{_configuration["Jwt:Issuer"]}/rescue-password?token={WebUtility.UrlEncode(token)}'>Create New Password</a>");


        return Ok(new
        {
          token = token,
          result = "success",
          message = "Your rescue code has been send your email address."
        });
      }
      else
      {
        return BadRequest(new
        {
          result = "error",
          message = "Oops! Entered email address could not be verified!"
        });
      }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateNewPassword([FromBody] RecoveryPasswordModel model)
    {
      var user = await _userManager.FindByEmailAsync(model.Email);
      if (user != null)
      {
        var result = await _userManager.ResetPasswordAsync(user, WebUtility.UrlDecode(model.Token), model.NewPassword);
        if (result.Succeeded)
        {
          await _emailService.SendEmail(user.Email, "bestdealpharma.com Change Password",
            "Your password has been changed successful");

          return Ok();
        }
        else
        {
          return BadRequest(result.Errors);
        }
      }
      else
      {
        return BadRequest();
      }
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }


      var jwt = new JwtSecurityTokenHandler().ReadJwtToken(model.Token);

      if (jwt.ValidTo <= DateTime.Now && jwt.Issuer == _configuration["Jwt:Issuer"])
      {
        var email = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
          var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
          if (result.Succeeded)
          {
            await _emailService.SendEmail(user.Email, "bestdealpharma.com Change Password",
              "Your password has been changed successful");

            return Ok();
          }
          else
          {
            return BadRequest(result.Errors);
          }
        }
        else
        {
          return BadRequest();
        }
      }
      else
      {
        return BadRequest();
      }
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
        PhoneNumber = model.MobilePhone
      };
      var result = await _userManager.CreateAsync(identifier, model.Password);

      if (result.Succeeded)
      {
        await _userManager.AddToRoleAsync(identifier, "Guest");
        await _signInManager.SignInAsync(identifier, false);
        var tokenString = GenerateJsonWebToken(identifier);

        var person = new Person()
        {
          Name = model.Name,
          Surname = model.Surname,
          Address = model.Address,
          State = model.State,
          City = model.City,
          Country = model.Country,
          BirthDate = model.BirthDate,
          HomePhone = model.HomePhone,
          MobilePhone = model.MobilePhone,
          ZipCode = model.ZipCode,
          UserId = identifier.Id
        };

        _context.People.Add(person);

        var address = new Address()
        {
          State = model.State,
          City = model.City,
          Country = model.Country,
          MobilePhone = model.MobilePhone,
          ZipCode = model.ZipCode,
          AddressLine = model.Address,
          Person = person,
          AddressName = "default"
        };

        _context.Addresses.Add(address);

        _context.SaveChanges();

        var response = Ok(new
        {
          token = tokenString,
          user = _authPerson.GetAuthenticatedUser(identifier.Email),
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

    public class UserChangePasswordModel
    {
      [Required] public string CurrentPassword { get; set; }
      [Required] public string NewPassword { get; set; }
      [Required] public string ConfirmPassword { get; set; }
      [Required] public string Token { get; set; }
    }

    public class RecoveryPasswordModel
    {
      [Required] public string Email { get; set; }
      [Required] public string NewPassword { get; set; }
      [Required] public string ConfirmPassword { get; set; }
      [Required] public string Token { get; set; }
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
