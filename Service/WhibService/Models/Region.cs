using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhibService.Models
{
  public class Region
  {
    public bool IsDeleted { get; set; }

    public RegionType RegionType { get; set; }

    public string EnglishName { get; set; }

    public string LocalName { get; set; }

    public string IsoCode2 { get; set; }

    public string IsoCode3 { get; set; }

    public decimal AreaSqKm { get; set; }

    public long Population { get; set; }

    public int Capital_CityId { get; set; }

    public int Largest_CityId { get; set; }
  }
}