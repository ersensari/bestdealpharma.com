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
  [Authorize(Roles = "Admin,Editor,Guest")]
  [ApiController]
  public class AddressesController : ControllerBase
  {
    private readonly Data.DbContext _context;

    public AddressesController(Data.DbContext context)
    {
      _context = context;
    }

    // GET: api/Addresses
    [HttpGet]
    [Route("api/Addresses")]
    public IActionResult GetAddresses() => Ok(_context.Addresses);

    // GET: api/Addresses/5
    [HttpGet]
    [Route("api/Addresses/{id}")]
    public async Task<IActionResult> GetAddress([FromRoute] int id)
    {
      if (id == -1) //isnew
      {
        return Ok(new Address());
      }

      var addr = await _context.Addresses.SingleOrDefaultAsync(m => m.Id == id);

      if (addr == null)
      {
        return NotFound(id);
      }

      return Ok(addr);
    }

    [HttpGet]
    [Route("api/Addresses/GetPersonAddresses/{id}")]
    public async Task<IActionResult> GetPersonAddress([FromRoute] int id)
    {
      var addr = await _context.Addresses.Where(m => m.PersonId == id).ToListAsync();
      return Ok(addr);
    }

    // PUT: api/Addresses/5
    [HttpPut]
    [Route("api/Addresses/{id}")]
    public async Task<IActionResult> PutAddress([FromRoute] int id, [FromBody] Address addr)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != addr.Id)
      {
        return BadRequest();
      }

      _context.Entry(addr).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AddrExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Ok(addr);
    }

    // POST: api/Addresses
    [HttpPost]
    [Route("api/Addresses")]
    public async Task<IActionResult> PostAddress([FromBody] Address addr)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Addresses.Add(addr);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetAddress", new {id = addr.Id}, addr);
    }

    // DELETE: api/Addresses/5
    [HttpDelete]
    [Route("api/Addresses/{id}")]
    public async Task<IActionResult> DeleteAddress([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var addr = await _context.Addresses.SingleOrDefaultAsync(m => m.Id == id);
      if (addr == null)
      {
        return NotFound();
      }

      _context.Addresses.Remove(addr);
      await _context.SaveChangesAsync();

      return Ok(addr);
    }

    private bool AddrExists(int id)
    {
      return _context.Addresses.Any(e => e.Id == id);
    }
  }
}
