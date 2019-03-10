using bestdealpharma.com.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Controllers.Api
{
  [Produces("application/json")]
  [Authorize(Roles = "Admin,Editor")]
  [ApiController]
  public class ShippingsController : ControllerBase
  {
    private readonly Data.DbContext _context;

    public ShippingsController(Data.DbContext context) => _context = context;

    // GET: api/Shippings
    [HttpGet]
    [AllowAnonymous]
    [Route("api/Shippings")]
    public IActionResult GetShippings() => Ok(_context.Shippings);

    // GET: api/Shippings/5
    [HttpGet]
    [Route("api/Shippings/{id}")]
    public async Task<IActionResult> GetShipping([FromRoute] int id)
    {
      if (id == -1) //isnew
      {
        return Ok(new Shipping());
      }

      var shipping = await _context.Shippings.SingleOrDefaultAsync(m => m.Id == id);

      if (shipping == null)
      {
        return NotFound(id);
      }

      return Ok(shipping);
    }

    // PUT: api/Shippings/5
    [HttpPut]
    [Route("api/Shippings/{id}")]
    public async Task<IActionResult> PutShipping([FromRoute] int id, [FromBody] Shipping shipping)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != shipping.Id)
      {
        return BadRequest();
      }

      _context.Entry(shipping).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ShippingExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Ok(shipping);
    }

    // POST: api/Shippings
    [HttpPost]
    [Route("api/Shippings")]
    public async Task<IActionResult> PostShipping([FromBody] Shipping shipping)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Shippings.Add(shipping);
      await _context.SaveChangesAsync();

      return CreatedAtAction($"GetShipping", new {id = shipping.Id}, shipping);
    }

    // DELETE: api/Shippings/5
    [HttpDelete]
    [Route("api/Shippings/{id}")]
    public async Task<IActionResult> DeleteShipping([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var shipping = await _context.Shippings.SingleOrDefaultAsync(m => m.Id == id);
      if (shipping == null)
      {
        return NotFound();
      }

      _context.Shippings.Remove(shipping);
      await _context.SaveChangesAsync();

      return Ok(shipping);
    }

    private bool ShippingExists(int id)
    {
      return _context.Shippings.Any(e => e.Id == id);
    }
  }
}
