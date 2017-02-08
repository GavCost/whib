namespace WhibServiceTest
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Linq;
  using System.Text;
  using System.Xml.Serialization;

  /// <summary>
  /// This class represents an region of the world, it could be the world itself, a continent, a country, or a region of a country.
  /// It is one that has two basic statistics, area and population.  Other statistics are added dymanically using the Statistic 
  /// and DataPoint class.
  /// </summary>
  /// <remarks>
  /// Name is the English Name of the Region.
  /// Information for countries is from http://www.geonames.org/countries/
  /// </remarks>
  [DebuggerDisplay("Name = {Name}, Id = {Id}")]
  public class Region : SummableObject
  {
    public enum RegionLevel
    {
      Unknown,
      World,
      Continent,
      Country,
      SubNational,
    }

    /// <summary>
    /// Represents the parent of this region.
    /// </summary>
    /// <remarks>
    /// The regional structure is a hierarchy, with the World as a root, therefore this is 0 for that.
    /// </remarks>
    [DefaultValue(0)]
    public int Parent_RegionId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public RegionLevel RegionType { get; set; }

    public override List<Statistic> Statistics { get; protected set; }

    public override List<Region> SubRegionList { get; protected set; }

    /// <summary>
    /// 
    /// </summary>
    public string Capital { get; set; }

    /// <summary>
    /// An list of codes for the regions.  These are formatted in the format (CodeName1,Code1),(CodeName2,Code2).
    /// </summary>
    public string Codes { get; set; }

    /// <summary>
    /// An list of alternate names for the regions.  These are formatted in the format (Name1),(Name2).
    /// </summary>
    public string AlternateNames { get; set; }

    public Region()
    {
    }

    public Region FindSubRegionById(int id)
    {
      if (this.Id == id)
      {
        return this;
      }

      Region region = this.SubRegionList.Find(x => x.Id == id);

      if (region == null)
      {
        foreach (Region subRegion in this.SubRegionList)
        {
          region = subRegion.FindSubRegionById(id);

          if (region != null)
          {
            break;
          }
        }
      }

      return region;
    }

    /// <summary>
    /// Finds a subregion by name, going recursive if not in the list.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <remarks>
    /// Regions are not necessarily uniquely named, so this should really be used only for one level down where
    /// they are fairly likely to be.
    /// </remarks>
    public Region FindSubRegionByName(string name)
    {
      Region region = this.SubRegionList.Find(x => x.Name == name);

      if (region == null)
      {
        foreach (Region subRegion in this.SubRegionList)
        {
          region = subRegion.FindSubRegionByName(name);

          if (region != null)
          {
            break;
          }
        }
      }

      return region;
    }

    public Region FindSubRegionByLevel(RegionLevel regionType, string name)
    {
      Region region = this.SubRegionList.Find(x => x.RegionType == regionType && x.Name == name);

      if (region == null)
      {
        foreach (Region subRegion in this.SubRegionList)
        {
          region = subRegion.FindSubRegionByLevel(regionType, name);

          if (region != null)
          {
            break;
          }
        }
      }

      return region;
    }
  }
}
