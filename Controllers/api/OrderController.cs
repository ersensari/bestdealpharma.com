using System;
using bestdealpharma.com.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bestdealpharma.com.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.CodeAnalysis.Formatting;

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
    private readonly IEmailService _emailService;

    public OrderController(Data.DbContext context, IHostingEnvironment hostingEnvironment,
      Providers.IAuthenticatedPersonProvider authPerson, IEmailService emailService)
    {
      _context = context;
      _hostingEnvironment = hostingEnvironment;
      _authPerson = authPerson;
      _emailService = emailService;
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
        var authMember = _authPerson.GetAuthenticatedUser(string.Empty);

        foreach (var item in order.OrderDetails)
        {
          item.Id = 0;
        }

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        string bodyHtml = CreateOrderMailBodyHtml(order);
        await _emailService.SendEmail(authMember.User.Email,
          $"bestdealpharma.com - Your order has been created #{order.OrderNumber}", bodyHtml);

        await _emailService.SendEmail("info@bestdealpharma.com",
          $"bestdealpharma.com - the order has been created #{order.OrderNumber}", bodyHtml);
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
        .OrderBy(x => x.Status)
        .ToListAsync());
    }

    [HttpGet]
    [Route("api/order/getAllOrders")]
    [Authorize(Roles = "Admin, Editor")]
    public async Task<IActionResult> GetAllOrders(int status)
    {
      return Ok(await _context.Orders
        .Include(x => x.OrderDetails)
        .Include(x => x.Person)
        .ThenInclude(x => x.User)
        .Include(x => x.Prescription)
        .Where(x => x.Status == status)
        .OrderBy(x => x.OrderDate)
        .ToListAsync());
    }

    [HttpPost]
    [Route("api/order/cancelOrder/{id}")]
    public async Task<IActionResult> CancelOrder(int id)
    {
      var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
      var authMember = _authPerson.GetAuthenticatedUser(string.Empty);
      if (order != null && order.PersonId == authMember.Person.Id)
      {
        order.Status = 3;
        await _context.SaveChangesAsync();
        return Ok(order);
      }
      else
      {
        return BadRequest();
      }
    }

    [HttpPost]
    [Route("api/order/archiveOrder/{id}")]
    public async Task<IActionResult> ArchiveOrder(int id)
    {
      var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
      var authMember = _authPerson.GetAuthenticatedUser(string.Empty);
      if (order != null && order.PersonId == authMember.Person.Id)
      {
        order.Archived = true;
        await _context.SaveChangesAsync();
        return Ok(order);
      }
      else
      {
        return BadRequest();
      }
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

    private string CreateOrderMailBodyHtml(Order order)
    {
      StringBuilder bodyHtml = new StringBuilder();
      bodyHtml.AppendLine(
        @"table { border-collapse: collapse; }
            table, th, td { border: 1px solid black; }");
      bodyHtml.AppendLine("<h3>Thank you for choosing BestDealPharma.com</h3>");
      if (order.Status == 2 && !string.IsNullOrWhiteSpace(order.ShippingLink))
      {
        bodyHtml.AppendLine($"<h4><a href='{order.ShippingLink}' target='_blank'>Trace your shipping</a></h4>");
      }

      string statusText = "";
      switch (order.Status)
      {
        case 0:
          statusText = "Waiting Payment";
          break;
        case 1:
          statusText = "Preparing";
          break;
        case 2:
          statusText = "Shipped";
          break;
        case 3:
          statusText = "Canceled";
          break;
        default:
          statusText = "";
          break;
      }

      bodyHtml.AppendLine("<table>" +
                          $"<tr><th>Order Status</th><td>{statusText}</td></tr>" +
                          $"<tr><th>Customer Name</th><td>{order.Person.Name} {order.Person.Surname}</td></tr>" +
                          $"<tr><th>Shipping Address</th><td>{order.AddressLine}, {order.ZipCode}, {order.City}, {order.State},{order.Country}</td></tr>" +
                          $"<tr><th>Phone Number</th><td>{order.MobilePhone}</td></tr>" +
                          $"<tr><th>Order Number</th><td>{order.OrderNumber}</td></tr>" +
                          $"<tr><th>Order Date</th><td>{order.OrderDate.ToShortDateString()}</td></tr>" +
                          $"<tr><th>Sub Total</th><td>${order.SubTotal}</td></tr>" +
                          $"<tr><th>Shipping</th><td>${order.Shipping}</td></tr>" +
                          $"<tr><th>Total</th><td>${order.Total}</td></tr>" +
                          "</table>");

      bodyHtml.AppendLine("<h4>Order Detail</h4><table>" +
                          $"<tr><th>Drug Name</th>" +
                          $"<th>Quantity</th>" +
                          $"<th>Strength</th>" +
                          $"<th>Amount</th>" +
                          $"<th>Price</th></tr>");

      foreach (var item in order.OrderDetails)
      {
        bodyHtml.AppendLine("<tr>" +
                            $"<td>{item.Title}</td>" +
                            $"<td>{item.Quantity}</td>" +
                            $"<td>{item.Strength}</td>" +
                            $"<td>{item.Amount}</td>" +
                            $"<td>${item.Price}</td>" +
                            "</tr>");
      }

      bodyHtml.AppendLine("</html>");


      return bodyHtml.ToString();
    }

    private bool OrderExists(int id)
    {
      return _context.Orders.Any(e => e.Id == id);
    }
  }
}
