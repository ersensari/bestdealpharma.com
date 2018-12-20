using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace bestdealpharma.com.Data.Models {
  public class Setting {
    public Guid Id { get; set; }
    public string SiteName { get; set; }
    public string Title { get; set; }
    public string Meta { get; set; }
    public string Description { get; set; }
    public string TopBarLogo { get; set; }
    public string FooterBarLogo { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public string Phone3 { get; set; }
    public string Email { get; set; }
    public string FacebookLink { get; set; }
    public string TwitterLink { get; set; }
    public string GooglePlusLink { get; set; }
    public string InstagramLink { get; set; }
    public string PinterestLink { get; set; }
    public string Address { get; set; }
    public string GoogleMapsCoordinates { get; set; }
    public string GoogleAnalyticsCode { get; set; }
    public string CopyrightText { get; set; }

    public string ExternalInfo1 { get; set; }
    public string ExternalInfo2 { get; set; }

    public int Season { get; set; }

    [NotMapped]
    public IFormFile TopBarLogoFile { get; set; }

    [NotMapped]
    public IFormFile FooterBarLogoFile { get; set; }

  }
}