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
  public class ProductsController : ControllerBase
  {
    private readonly Data.DbContext _context;

    public ProductsController(Data.DbContext context)
    {
      _context = context;
    }

    // GET: api/Products
    [HttpGet]
    [AllowAnonymous]
    [Route("api/Products")]
    public IActionResult GetProducts() => Ok(_context.Products);

    // GET: api/Products/5
    [HttpGet]
    [Route("api/Products/{id}")]
    public async Task<IActionResult> GetProduct([FromRoute] int id)
    {
      if (id == -1) // isnew
      {
        return Ok(new Product());
      }

      var p = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);

      if (p == null)
      {
        return NotFound(id);
      }

      return Ok(p);
    }

    // PUT: api/Products/5
    [HttpPut]
    [Route("api/Products/{id}")]
    public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != product.Id)
      {
        return BadRequest();
      }

      _context.Entry(product).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProductExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Ok(product);
    }

    // POST: api/Products
    [HttpPost]
    [Route("api/Products")]
    public async Task<IActionResult> PostProduct([FromBody] Product product)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Products.Add(product);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProduct", new {id = product.Id}, product);
    }

    // DELETE: api/Products/5
    [HttpDelete]
    [Route("api/Products/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
      if (product == null)
      {
        return NotFound();
      }

      _context.Products.Remove(product);
      await _context.SaveChangesAsync();

      return Ok(product);
    }

    [HttpGet]
    [Route("api/Products/find")]
    [AllowAnonymous]
    public async Task<IActionResult> FindProducts(string keyword, bool byLetter)
    {
      var linq = from p in _context.Products
        where p.IsAvailable && (byLetter ? p.Title.StartsWith(keyword) : p.Title.Contains(keyword)) == true
        select p;

      return Ok(await linq.ToListAsync());
    }

    private bool ProductExists(int id)
    {
      return _context.Products.Any(e => e.Id == id);
    }
  }
}
