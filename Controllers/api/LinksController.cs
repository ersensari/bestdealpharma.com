using bestdealpharma.com.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bestdealpharma.com.Controllers.api
{
  [Produces("application/json")]
  [Authorize(Roles = "Admin,Editor")]
  [ApiController]
  public class LinksController : ControllerBase
  {
    private readonly Data.DbContext _context;

    public LinksController(Data.DbContext context)
    {
      _context = context;
    }

    // GET: api/Links
    [HttpGet]
    [AllowAnonymous]
    [Route("api/Links")]
    public IActionResult GetLinks(int? layout) =>
      Ok(_context.Links
        .Where(x => !layout.HasValue || x.Layout == layout.Value)
        .Include(x => x.Page).ThenInclude(x => x.Galleries).OrderBy(x => x.DisplayOrder));

    // GET: api/Links/5
    [HttpGet]
    [AllowAnonymous]
    [Route("api/Links/{id}")]
    public async Task<IActionResult> GetLink([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id == -1)//isnew
      {
        return Ok(new Link());
      }


      var link = await _context.Links.Include(x => x.Page).ThenInclude(x => x.Galleries).SingleOrDefaultAsync(m => m.Id == id);

      if (link == null)
      {
        return NotFound();
      }

      return Ok(link);
    }



    // PUT: api/Links/5
    [HttpPut]
    [Route("api/Links/{id}")]
    public async Task<IActionResult> PutLink([FromRoute] int id, [FromBody] Link link)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != link.Id)
      {
        return BadRequest();
      }

      _context.Entry(link).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!LinkExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return Ok(link);
    }

    // POST: api/Links
    [HttpPost]
    [Route("api/Links")]
    public async Task<IActionResult> PostLink([FromBody] Link link)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Links.Add(link);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetLink", new { id = link.Id }, link);
    }

    // DELETE: api/Links/5
    [HttpDelete]
    [Route("api/Links/{id}")]
    public async Task<IActionResult> DeleteLink([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var link = await _context.Links.SingleOrDefaultAsync(m => m.Id == id);
      if (link == null)
      {
        return NotFound();
      }

      _context.Links.Remove(link);
      await _context.SaveChangesAsync();

      return Ok(link);
    }

    private bool LinkExists(int id)
    {
      return _context.Links.Any(e => e.Id == id);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("api/Links/GetLinkHierarchy")]
    public IActionResult GetLinkHierarchy(int layout, string url)
    {
      List<Link> AllLinks = new List<Link>();
      var rootlinks = _context.Links.Include(x => x.Page).ThenInclude(x => x.Galleries)
        .Where(x => string.IsNullOrWhiteSpace(url) || x.Url == url)
        .Where(x => !x.ParentId.HasValue && x.Layout == layout).OrderBy(x => x.DisplayOrder);
      foreach (var item in rootlinks)
      {
        AllLinks.Add(GetLinkChildren(item));
      }

      return Ok(AllLinks);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("api/Links/GetByUrl")]
    public IActionResult GetByUrl(int layout, string url)
    {
      List<Link> AllLinks = new List<Link>();
      var rootlinks = _context.Links.Include(x => x.Page).ThenInclude(x => x.Galleries)
        .Where(x => string.IsNullOrWhiteSpace(url) || x.Url == url)
        .Where(x => x.Layout == layout).OrderBy(x => x.DisplayOrder);
      foreach (var item in rootlinks)
      {
        AllLinks.Add(GetLinkChildren(item));
      }

      return Ok(AllLinks.FirstOrDefault());
    }


    internal Link GetLinkChildren(Link parent)
    {
      var childlinks = _context.Links.Include(x => x.Page).ThenInclude(x => x.Galleries).Where(x => x.ParentId == parent.Id).OrderBy(x => x.DisplayOrder);
      foreach (var item in childlinks)
      {
        if (_context.Links.Any(x => x.ParentId == item.Id))
        {
          if (parent.Children == null)
            parent.Children = new List<Link>();
          parent.Children.Add(GetLinkChildren(item));
        }
        else
        {
          if (parent.Children == null)
            parent.Children = new List<Link>();
          parent.Children.Add(item);
        }
      }
      return parent;
    }
  }
}
