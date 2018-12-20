using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace bestdealpharma.com
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<Data.DbContext>(options =>
                                      options
                                      .UseSqlServer(Configuration.GetConnectionString("dbContext"))
                                      .ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning))
                                      );


      services.AddIdentity<IdentityUser, IdentityRole>()
          .AddEntityFrameworkStores<Data.DbContext>()
          .AddDefaultTokenProviders();

      services.ConfigureApplicationCookie(options =>
      {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
        options.Cookie = new CookieBuilder
        {
          HttpOnly = true,
          Name = "bestdealpharma.com.Security.Cookie",
          Path = "/",
          SameSite = SameSiteMode.Lax,
          SecurePolicy = CookieSecurePolicy.SameAsRequest
        };
      });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("AllRoles", policy =>
               policy.RequireRole("Admin", "Editor", "Call_Center"));
      });

      services.AddMvc().AddJsonOptions(
        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
      );

      // Add framework services.
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      // Simple example with dependency injection for a data provider.
      services.AddSingleton<Providers.IWeatherProvider, Providers.WeatherProviderFake>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, Data.DbContext dbContext)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();

        // Webpack initialization with hot-reload.
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true,
        });
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseAuthentication();

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapSpaFallbackRoute(
                  name: "spa-fallback",
                  defaults: new { controller = "Home", action = "Index" });

        routes.MapSpaFallbackRoute(
                  name: "admin-spa-fallback",
                  defaults: new { controller = "Admin", action = "Index" });
      });

      // ===== Create tables ======
      dbContext.Database.EnsureCreated();
    }
  }
}
