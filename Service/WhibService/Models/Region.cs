namespace WhibService.Models
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.Linq;
  using System.Web;

  /// <summary>
  /// This class represents an region of the world, it could be the world itself, a continent, a country, or a region of a country.
  /// It is one that has two basic statistics, area and population.
  /// </summary>
  [DebuggerDisplay("Name = {EnglishName}, Id = {Id}")]
  public class Region
  {
    public int Id { get; set; }

    public bool IsDeleted { get; set; }

    public RegionType RegionType { get; set; }

    public int? ParentId { get; set; }

    public string ParentName { get; set; }

    public string EnglishName { get; set; }

    public string LocalName { get; set; }

    public string IsoCode2 { get; set; }

    public string IsoCode3 { get; set; }

    public decimal AreaSqKm { get; set; }

    public long Population { get; set; }

    public int? Capital_CityId { get; set; }

    public int? Largest_CityId { get; set; }
  }
}