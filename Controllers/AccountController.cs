using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace bestdealpharma.com.Controllers
{
  [Route("[controller]/[action]")]
  public class AccountController : Controller
  {
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration
        )
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _configuration = configuration;
    }

    [HttpPost]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody]UserModel login)
    {
      IActionResult response = Unauthorized();
      var user = await AuthenticateUserAsync(login);

      if (user != null)
      {
        var tokenString = GenerateJSONWebToken(user);
        response = Ok(new { token = tokenString });
      }

      return response;
    }

    private async Task<UserModel> AuthenticateUserAsync(UserModel model)
    {
      UserModel user = null;

      var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
      if (result.Succeeded)
      {
        user = new UserModel { Email = model.Email };
      }
      return user;
    }

    //[HttpPost]
    //public async Task<object> Register([FromBody] RegisterDto model)
    //{
    //  if (!ModelState.IsValid)
    //  {
    //    return BadRequest(ModelState);
    //  }

    //  var user = new IdentityUser
    //  {
    //    UserName = model.Email,
    //    Email = model.Email
    //  };
    //  var result = await _userManager.CreateAsync(user, model.Password);

    //  if (result.Succeeded)
    //  {
    //    await _signInManager.SignInAsync(user, false);
    //    return GenerateJSONWebToken(model.Email, user);
    //  }

    //  throw new ApplicationException("UNKNOWN_ERROR");
    //}

    [Authorize]
    [HttpGet]
    public async Task<object> Protected()
    {
      return "Protected area";
    }

    private string GenerateJSONWebToken(UserModel userInfo)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
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
      [Required]
      public string Email { get; set; }

      [Required]
      public string Password { get; set; }

    }

    public class RegisterDto
    {
      [Required]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
      public string Password { get; set; }
    }
  }
}
