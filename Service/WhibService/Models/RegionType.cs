using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhibService.Models
{
  /// <summary>
  /// This enum represents the types of regions.
  /// </summary>
  public enum RegionType
  {
    Unknown = 0,

    World,

    Continent,

    Country,

    SubNational,
  }
}