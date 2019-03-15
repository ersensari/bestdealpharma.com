using System;
using bestdealpharma.com.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bestdealpharma.com.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace bestdealpharma.com.Controllers.Api
{
  [Produces("application/json")]
  [Authorize(Roles = "Admin,Editor,Guest")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly Data.DbContext _context;
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly Providers.IAuthenticatedPersonProvider _authPerson;

    public OrderController(Data.DbContext context, IHostingEnvironment hostingEnvironment,
      Providers.IAuthenticatedPersonProvider authPerson)
    {
      _context = context;
      _hostingEnvironment = hostingEnvironment;
      _authPerson = authPerson;
    }

    [HttpPost]
    [Route("api/order/upload")]
    public ActionResult Upload(IFormFile file)
    {
      if (file != null)
      {
        var authMember = _authPerson.GetAuthenticatedUser(string.Empty);
        string webRootPath = _hostingEnvironment.WebRootPath;
        string fileName =
          UploadHelper.UploadSingle(file, 0, "_prescription", webRootPath, "uploads", "prescriptions",
            authMember.User.Email);
        var prescription = new Prescription();
        prescription.PersonId = authMember.Person.Id;
        prescription.Name = file.FileName;
        prescription.FileName = fileName;
        prescription.UploadDate = DateTime.Now;

        _context.Prescriptions.Add(prescription);
        _context.SaveChanges();
      }
      else
      {
        return BadRequest();
      }

      return Ok(file);
    }

    [HttpGet]
    [Route("api/order/getPrescription")]
    public async Task<IActionResult> GetPrescription()
    {
      var authMember = _authPerson.GetAuthenticatedUser(string.Empty);

      var list = await _context.Prescriptions.Where(x => x.PersonId == authMember.Person.Id).ToListAsync();

      return Ok(list);
    }

    [HttpPost]
    [Route("api/order/createOrder")]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
      if (ModelState.IsValid)
      {
        foreach (var item in order.OrderDetails)
        {
          item.Id = 0;
        }

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
      }
      else
      {
        return BadRequest();
      }

      return Ok();
    }

    [HttpGet]
    [Route("api/order/getOrders")]
    public async Task<IActionResult> GetOrders()
    {
      var authMember = _authPerson.GetAuthenticatedUser(string.Empty);

      return Ok(await _context.Orders.Include(x => x.OrderDetails).Where(x => x.PersonId == authMember.Person.Id)
        .ToListAsync());
    }

    [HttpGet]
    [Route("api/order/getAllOrders")]
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> GetAllOrders(int status)
    {
      return Ok(await _context.Orders
        .Include(x => x.OrderDetails)
        .Include(x=>x.Person)
        .Include(x=>x.Prescription)
        .Where(x => x.Status == status).ToListAsync());
    }

    // PUT: api/Orders/5
    [HttpPut]
    [Route("api/order/{id}")]
    public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] Order order)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != order.Id)
      {
        return BadRequest();
      }

      _context.Entry(order).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!OrderExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Ok(order);
    }


    private bool OrderExists(int id)
    {
      return _context.Orders.Any(e => e.Id == id);
    }

  }
}
