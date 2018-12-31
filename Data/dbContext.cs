using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using bestdealpharma.com.Data.Models;

namespace bestdealpharma.com.Data
{
  public class DbContext : IdentityDbContext
  {
    public DbContext(DbContextOptions<DbContext> options) : base(options) { }

    public DbSet<Person> People { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Gallery> Galleries { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<ImageSlider> ImageSliders { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Town> Towns { get; set; }
    public DbSet<ContactForm> ContactForms { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<CurrencyRate> CurrencyRates { get; set; }
    public DbSet<Product> Products { get; set; }
  }
}
