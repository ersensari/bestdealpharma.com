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
  public class PagesController : ControllerBase
  {
    private readonly Data.DbContext _context;

    public PagesController(Data.DbContext context)
    {
      _context = context;
    }

    // GET: api/Pages
    [HttpGet]
    [AllowAnonymous]
    [Route("api/Pages")]
    public IActionResult GetPages() => Ok(_context.Pages);

    // GET: api/Pages/5
    [HttpGet]
    [Route("api/Pages/{id}")]
    public async Task<IActionResult> GetPage([FromRoute] int id)
    {
      if (id == -1) //isnew
      {
        return Ok(new Page());
      }

      var page = await _context.Pages.SingleOrDefaultAsync(m => m.Id == id);

      if (page == null)
      {
        return NotFound(id);
      }

      return Ok(page);
    }

    // PUT: api/Pages/5
    [HttpPut]
    [Route("api/Pages/{id}")]
    public async Task<IActionResult> PutPage([FromRoute] int id, [FromBody] Page page)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != page.Id)
      {
        return BadRequest();
      }

      _context.Entry(page).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PageExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Ok(page);
    }

    // POST: api/Pages
    [HttpPost]
    [Route("api/Pages")]
    public async Task<IActionResult> PostPage([FromBody] Page page)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Pages.Add(page);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetPage", new { id = page.Id }, page);
    }

    // DELETE: api/Pages/5
    [HttpDelete]
    [Route("api/Pages/{id}")]
    public async Task<IActionResult> DeletePage([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var page = await _context.Pages.SingleOrDefaultAsync(m => m.Id == id);
      if (page == null)
      {
        return NotFound();
      }


      var relatedLinks = _context.Links.Where(x => x.PageId == id);
      foreach (var link in relatedLinks)
      {
        link.PageId = null;
      }

      _context.Pages.Remove(page);
      await _context.SaveChangesAsync();

      return Ok(page);
    }

    private bool PageExists(int id)
    {
      return _context.Pages.Any(e => e.Id == id);
    }
  }
}

