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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace bestdealpharma.com
{
  public class Startup
  {
    private readonly ILogger _logger;

    public Startup(IConfiguration configuration, ILogger<Startup> logger)
    {
      Configuration = configuration;
      _logger = logger;

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

      _logger.LogInformation("bestdealpharma.com");


      services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<Data.DbContext>()
        .AddDefaultTokenProviders();

      services.ConfigureApplicationCookie(options =>
      {
        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
        {
          OnRedirectToLogin = (context) =>
          {
            context.Response.StatusCode = 401;
            return System.Threading.Tasks.Task.CompletedTask;
          },
        };
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
        options.AddPolicy("OnlyAdminRights", policy =>
          policy.RequireRole("Admin", "Editor", "Call_Center"));
      });

      services.AddMvc().AddJsonOptions(
        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
      );

      // Add framework services.
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.AddScoped<Providers.IAuthenticatedPersonProvider, Providers.AuthenticatedPersonProvider>((ctx) =>
      {
        var datacontext = ctx.GetService<Data.DbContext>();
        var _httpContextAccessor = ctx.GetService<IHttpContextAccessor>();

        return new Providers.AuthenticatedPersonProvider(datacontext, _httpContextAccessor);
      });
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
        app.UseDeveloperExceptionPage();

        //app.UseExceptionHandler("/Home/Error");
      }

      app.UseStatusCodePages();

      app.UseAuthentication();

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapRoute(
          name: "api",
          template: "api/{controlle}/{action}/{id?}");

        routes.MapSpaFallbackRoute(
          name: "spa-fallback",
          defaults: new {controller = "Home", action = "Index"});

        routes.MapSpaFallbackRoute(
          name: "admin-spa-fallback",
          defaults: new {controller = "Admin", action = "Index"});
      });

      // ===== Create tables ======
      dbContext.Database.EnsureCreated();
    }
  }
}
